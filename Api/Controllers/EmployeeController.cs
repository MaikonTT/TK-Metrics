using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class EmployeeController : Controller
    {
        [HttpGet("{id}")]
        public ActionResult GetEmployee(int id)
        {
            var employee = new Employee
            {
                Id = id,
                Name = "Peter Parker",
                Role = "Fotógrafo",
                Salary = 650.50M
            };

            return Ok(employee);
        }

        [HttpPost("register")]
        public ActionResult Employee(Employee newEmployee)
        {
            var employee = new Employee
            {
                Id = 1,
                Name = newEmployee.Name,
                Role = newEmployee.Role,
                Salary = newEmployee.Salary
            };

            return Ok(employee);
        }
    }
}