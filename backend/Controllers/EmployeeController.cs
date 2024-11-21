using backend.DTO;
using backend.DTO.paginated;
using backend.DTO.request;
using backend.DTO.response;
using backend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/v1/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private EmployeeService employeeService;
        public EmployeeController(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpPost("add-employee")]
        public IActionResult addEmployee([FromBody] AddEmployeeDTO addEmployeeDTO)
        {
            GetEmployeeDTO getEmployeeDTO = employeeService.addEmployee(addEmployeeDTO);
            StandardResponse response = new StandardResponse(
                201,
                "ADDED SUCCESSFULLY",
                getEmployeeDTO);
            return Ok(response);
        }

        [HttpPut("update-employee")]
        public IActionResult updateEmployee([FromBody] AddEmployeeDTO addEmployeeDTO, [FromQuery] int id)
        {
            GetEmployeeDTO getEmployeeDTO = employeeService.updateEmployee(addEmployeeDTO, id);
            StandardResponse response = new StandardResponse(
                200,
                "UPDATED SUCCESSFULLY",
                getEmployeeDTO);
            return Ok(response);
        }

        [HttpDelete("delete-employee")]
        public IActionResult deleteEmployee([FromQuery] int id) 
        { 
            GetEmployeeDTO getEmployeeDTO = employeeService.deleteEmployee(id);
            StandardResponse response = new StandardResponse(
                200,
                "DELETED SUCCESSFULLY",
                getEmployeeDTO);
            return Ok(response);
        }

        [HttpGet("get-employee-by-id")]
        public IActionResult getEmployeeById([FromQuery] int id)
        {
            GetEmployeeDTO getEmployeeDTO = employeeService.getEmployeeById(id);
            StandardResponse response = new StandardResponse(
                200,
                "RETRIEVED SUCCESSFULLY",
                getEmployeeDTO);
            return Ok(response);
        }


        [HttpGet("get-all-employees")]
        public IActionResult getAllEmplyees([FromQuery] int page, [FromQuery] int pageSize) 
        {
            EmployeePaginatedDTO employeePaginatedDTO = employeeService.getAllEmployees(page, pageSize);
            StandardResponse response = new StandardResponse(
                200,
                "RETRIEVED SUCCESSFULLY",
                employeePaginatedDTO);
            return Ok(response);
        }

        [HttpGet("get-employees-by-position")]
        public IActionResult getEmployeesByPosition([FromQuery] string position, [FromQuery] int pageSize)
        {
            EmployeePaginatedDTO employeePaginatedDTO = employeeService.getEmployeesByPosition(position,pageSize);
            StandardResponse response = new StandardResponse(
                200,
                "RETRIEVED SUCCESSFULLY",
                employeePaginatedDTO);
            return Ok(response);
        }
    }
}
