using System;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Infrastructure.Interception;

namespace ContosoUniversity.DAL
{
    /**
     * Class handles Error checking, and also registers dummy errors to ensure it works.s
     */
    public class SchoolConfiguration : DbConfiguration
    {
        public SchoolConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
            DbInterception.Add(new SchoolInterceptorTransientErrors("Throw", 20));
            //DbInterception.Add(new SchoolInterceptorTransientErrors());
            DbInterception.Add(new SchoolInterceptorLogging());
        }
    }
}