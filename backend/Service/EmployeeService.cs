using backend.DTO.paginated;
using backend.DTO.request;
using backend.DTO.response;

namespace backend.Service
{
    public interface EmployeeService
    {
        GetEmployeeDTO addEmployee(AddEmployeeDTO addEmployeeDTO);
        GetEmployeeDTO deleteEmployee(int id);
        EmployeePaginatedDTO getAllEmployees(int page, int pageSize);
        GetEmployeeDTO getEmployeeById(int id);
        EmployeePaginatedDTO getEmployeesByPosition(string position, int pageSize);
        GetEmployeeDTO updateEmployee(AddEmployeeDTO updateEmployeeDTO, int id);
    }
}
