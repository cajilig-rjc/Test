using Core.Data.DAL;
using Core.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly IEmployee _repo;
        private readonly ILogger _logger;
        public TestsController(TestDbRepository repo,ILogger<TestsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        [HttpGet()]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var employees = await _repo.GetEmployees();

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
                var employee = await _repo.GetEmployee(id);

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
                
                 return StatusCode(200, await _repo.AddEmployee(employee));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        [HttpPost("employee/update")]
        public async Task<IActionResult> UpdateAsync([FromBody] Employee employee)
        {
            try
            {
               
              
                return StatusCode(200, await _repo.UpdateEmployee(employee));
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
               
                
                return StatusCode(200,await _repo.DeleteEmployee(id));
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
