using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Linq;
using ContosoUniversity.Logging;

namespace ContosoUniversity.DAL
{
    /**
     * To check for muliple errors, make a different instance of this class that checks for a 
     * different trigger word in ReaderExecuting() (this uses "Throw", use something else) and has a different 
     */
    public class SchoolInterceptorTransientErrors : DbCommandInterceptor
    {
        private string errorKey;
        private int errorNumber;
        private int _counter = 0;
        private ILogger _logger = new Logger();


        public SchoolInterceptorTransientErrors()
            : base()
        {
            errorKey = "Throw";
            errorNumber = 20;
        }
        public SchoolInterceptorTransientErrors(string key, int err)
            : base()
        {
            errorKey = key;
            errorNumber = err;
        }

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            bool throwTransientErrors = false;
            if (command.Parameters.Count > 0 && command.Parameters[0].Value.ToString() == errorKey)
            {
                throwTransientErrors = true;
                command.Parameters[0].Value = "an";
                command.Parameters[1].Value = "an";
            }

            if (throwTransientErrors && _counter < 4)
            {
                _logger.Information("Returning transient error for command: {0}", command.CommandText);
                _counter++;
                interceptionContext.Exception = CreateDummySqlException();
            }
        }

        private SqlException CreateDummySqlException()
        {
            // The instance of SQL Server you attempted to connect to does not support encryption
            var sqlErrorNumber = errorNumber;

            var sqlErrorCtor = typeof(SqlError).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Where(c => c.GetParameters().Count() == 7).Single();
            var sqlError = sqlErrorCtor.Invoke(new object[] { sqlErrorNumber, (byte)0, (byte)0, "", "", "", 1 });

            var errorCollection = Activator.CreateInstance(typeof(SqlErrorCollection), true);
            var addMethod = typeof(SqlErrorCollection).GetMethod("Add", BindingFlags.Instance | BindingFlags.NonPublic);
            addMethod.Invoke(errorCollection, new[] { sqlError });

            var sqlExceptionCtor = typeof(SqlException).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Where(c => c.GetParameters().Count() == 4).Single();
            var sqlException = (SqlException)sqlExceptionCtor.Invoke(new object[] { "Dummy", errorCollection, null, Guid.NewGuid() });

            return sqlException;
        }
    }
}