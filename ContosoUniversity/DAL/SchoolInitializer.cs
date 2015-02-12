using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ContosoUniversity.Models;

namespace ContosoUniversity.DAL
{
    public class SchoolInitializer : System.Data.Entity.DropCreateDatabaseAlways<SchoolContext>
    //public class SchoolInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SchoolContext>
    {
        protected override void Seed(SchoolContext context)
        {
            var students = new List<Student>
            {
                new Student {FirstMidName = "Aaron", LastName = "Carson", EnrollmentDate = DateTime.Parse("2012-04-01"), Email = "tepidUntruths15@mail.com"},
                new Student {FirstMidName = "Lady", LastName = "Gaga", EnrollmentDate = DateTime.Parse("2014-09-21"), Email = "unbaptisedFundamentalist8@gmail.com"},
                new Student {FirstMidName = "Robert", LastName = "Downey Jr", EnrollmentDate = DateTime.Parse("1999-04-01"), Email = "exhilarator68@wou.edu"}
            };
            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();
            
            var courses = new List<Course>
            {
                new Course {CourseID = 260, Title = "Data Structures I", Credits = 3, Description = "Study things such as lists, queues, stacks, nodes, trees, stumps, rocks, and heaps.  Also, learn why they are important.  Some of these may not actually be data structures used in the class - take it and find out."},
                new Course {CourseID = 311, Title = "Data Structures II", Credits = 3, Description = "Probably the hardest class in the major, as told by many.  Pass this obstacle and you can run the rest of the gauntlet, for sure."},
                new Course {CourseID = 262, Title = "Programming Languages - Haskell", Credits = 2, Description = "Like Java or C++?  OOP and iterative code your thing?  Take this class and blow your mind at how frustratingly short your code really can/should be."},
                new Course {CourseID = 271, Title = "Computer Architecture", Credits = 4, Description = "Practice your algebra skills as you learn about bits, bytes, buses, and many minute details about computer architecture you may have never known existed."},
                new Course {CourseID = 470, Title = "Human Machine Interfaces", Credits = 3, Description = "Using Visual Studio, learn how to write C# code using MVC 5 and EF 6.  Also, practice your presentation and teamwork skills."}
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment {StudentID = 1, CourseID = 262, Grade = Grade.A},
                new Enrollment {StudentID = 1, CourseID = 271, Grade = Grade.A},
                new Enrollment {StudentID = 1, CourseID = 470, Grade = Grade.A},
                new Enrollment {StudentID = 2, CourseID = 311, Grade = Grade.D},
                new Enrollment {StudentID = 3, CourseID = 260, Grade = Grade.B},
                new Enrollment {StudentID = 3, CourseID = 262, Grade = Grade.C}
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }
    }
}