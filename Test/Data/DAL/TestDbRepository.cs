using Core.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Data.DAL
{
    public class TestDbRepository : IEmployee
    {
        private readonly TestDbContext _context;
        public TestDbRepository(TestDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddEmployee(Employee employee)
        {
            _context.Add(employee);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteEmployee(int id)
        {
            var _employee = _context.Employees.Where(e => e.Id == id).FirstOrDefaultAsync();
            if (_employee == null)
                return 0;


            _context.Remove(_employee);
            return await _context.SaveChangesAsync();
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _context.Employees.Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<int> UpdateEmployee(Employee employee)
        {
            var _employee = _context.Employees.Where(e => e.Id == employee.Id).FirstOrDefaultAsync();
            if (_employee != null)
                _context.Entry(employee).State = EntityState.Detached;
            else
                return 0;

            _context.Entry(employee).State = EntityState.Modified;
           return await _context.SaveChangesAsync();
        }
    }
}
