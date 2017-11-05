using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplication1.Models
{
    public class Employee
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public int Age { set; get; }
    }

    public class EmployeeDBContent : DbContext
    {
        public DbSet<Employee> Employees { set; get; }
    }
}