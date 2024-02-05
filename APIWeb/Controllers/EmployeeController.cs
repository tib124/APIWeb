using APIWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employee;

        public EmployeeController(IEmployeeRepository employee)
        {
            _employee = employee;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        {
            try
            {
                return Ok(await _employee.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<Employee>>GetById(Guid id)
        {
            var employee = await _employee.GetById(id);

            if(employee != null)
            {
                return Ok(employee);
            }
            return BadRequest();
        }


        [HttpPost]
        public async Task<ActionResult<Employee>>AddEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }

                //var e = await _employee.GetByEmail(employee.Email);

                //if (e != null) 
                //{ 
                
                //    return BadRequest("Email In use");

                //}

                    var createemployee = await _employee.AddEmployee(employee);
                    return CreatedAtAction(nameof(GetAll), new { id = createemployee.IdEmployee }, createemployee);
                

          

            }
            catch
            {
                return BadRequest();
            }
        }



        [HttpPut("{id}")]

        public async Task<ActionResult<Employee>> UpdateEmployee(Guid id, Employee employee)
        {
            try
            {
                if (id != employee.IdEmployee)
                {
                    return BadRequest("Student ID Does Not Match");
                }

                var studentToUpdate = await _employee.GetById(id);

                if (studentToUpdate == null)
                {
                    return NotFound();
                }

                return await _employee.UpdateEmployee(employee);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                var studentToDelete = await _employee.GetById(id);

                if (studentToDelete == null)
                {
                    return NotFound($"Student with id = {id} not found");
                }

                await _employee.DeleteEmployee(id);

                return Ok($"Student with id = {id} deleted");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
