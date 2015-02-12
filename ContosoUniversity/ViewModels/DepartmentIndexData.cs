using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContosoUniversity.Models;

namespace ContosoUniversity.ViewModels
{
    public class DepartmentIndexData
    {
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}