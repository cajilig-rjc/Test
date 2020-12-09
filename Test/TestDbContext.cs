using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;

namespace Test
{
    public class TestDbContext:DbContext
    {
        public TestDbContext() : base(DbContextOptions <TestDbContext> options) { }
        public DbSet<Employee> Employees { get; set; }
        
    }
}
