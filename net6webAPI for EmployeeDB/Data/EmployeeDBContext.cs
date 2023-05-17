using System;
using net6webAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace net6webAPI.Data
{
    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }

        public DbSet<Department> Department { get; set; }
    }
}

