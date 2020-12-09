using Core.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Data
{
    public class TestDbContext:DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
           : base(options) { }
        public DbSet<Employee> Employees { get; set; }
    }
}
