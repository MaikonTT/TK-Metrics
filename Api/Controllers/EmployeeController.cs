using App.Metrics;
using Domain.Models;
using Domain.Providers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class EmployeeController : Controller
    {
        private readonly IMetricsProvider _metrics;
        private const string EmployeeCounter = "Employee Hits";
        private const string EmployeeTimer = "Employee Timer";

        public EmployeeController(IMetricsProvider metrics)
        {
            _metrics = metrics;
            _metrics.InitializeConfig(EmployeeCounter, EmployeeTimer);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployee(int id)
        {

            _metrics.SetMetricTags(new string[] { "method" }, new string[] { "get" });

            var employee = _metrics.ExecuteTimerMeasureWithResult
                (() => GetEmployeeInfo(id), "Employee");

            await _metrics.RegisterMetrics();

            return Ok(employee);
        }

        [HttpPost("register")]
        public ActionResult Employee(Employee newEmployee)
        {
            var employee = _metrics.ExecuteTimerMeasureWithResult(
                            () => GetEmployeeInfo(newEmployee), "EmployeeValue");

            return Ok(employee);
        }
        private static Employee GetEmployeeInfo(int id)
           => new()
           {
               Id = id,
               Name = "Peter Parker",
               Role = "Fotógrafo",
               Salary = 650.50M
           };

        private static Employee GetEmployeeInfo(Employee newEmployee)
            => new()
            {
                Id = 1,
                Name = newEmployee.Name ?? "Peter Parker",
                Role = newEmployee.Role ?? "Fotógrafo",
                Salary = newEmployee.Salary
            };
    }
}