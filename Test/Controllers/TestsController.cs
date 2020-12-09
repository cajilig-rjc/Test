using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly TestDbContext _context;
        private readonly ILogger _logger;
        public TestsController(TestDbContext context,ILogger<TestsController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet()]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var employees = await _context.Employees.ToListAsync();

                if (employees.Count == 0)
                    return StatusCode(StatusCodes.Status204NoContent);

                return StatusCode(200, employees);


            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
            
        }
        [HttpGet("employee")]
        public async Task<IActionResult> GetAsync([FromHeader] int id)
        {
            try
            {
                var employee = await _context.Employees.Where(e => e.Id == id).FirstOrDefaultAsync();

                if (employee== null)
                    return StatusCode(StatusCodes.Status204NoContent);

                return StatusCode(200, employee);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }

        }
        [HttpPost("employee/add")]
        public async Task<IActionResult> AddAsync([FromBody] Employee employee)
        {
            try
            {
                _context.Add(employee);
                 await _context.SaveChangesAsync();
                 return StatusCode(200, employee.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }

        }
        [HttpPost("employee/update")]
        public async Task<IActionResult> UpdateAsync([FromBody] Employee employee)
        {
            try
            {
                var _employee = _context.Employees.Where(e => e.Id == employee.Id).FirstOrDefaultAsync();
                if (_employee != null)
                    _context.Entry(employee).State = EntityState.Detached;
                else
                    return StatusCode(StatusCodes.Status204NoContent);

                _context.Entry(employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return StatusCode(200, employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }

        }
        [HttpDelete("employee/delete")]
        public async Task<IActionResult> DeleteAsync([FromHeader] int id)
        {
            try
            {
                var _employee = _context.Employees.Where(e => e.Id == id).FirstOrDefaultAsync();
                if (_employee == null)
                    return StatusCode(StatusCodes.Status204NoContent);


                _context.Remove(_employee);
                await _context.SaveChangesAsync();
                
                return StatusCode(200, _employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }

        }
    }
}
