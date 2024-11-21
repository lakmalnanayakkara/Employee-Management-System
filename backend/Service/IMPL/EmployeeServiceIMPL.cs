using AutoMapper;
using backend.DTO.paginated;
using backend.DTO.request;
using backend.DTO.response;
using backend.Entity;
using backend.Exception;
using backend.Repository;
using Microsoft.EntityFrameworkCore;

namespace backend.Service.IMPL
{
    public class EmployeeServiceIMPL : EmployeeService
    {
        private AppDbContext dbContext;
        private IMapper mapper;
        public EmployeeServiceIMPL(AppDbContext dbContext, IMapper mapper) 
        { 
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public GetEmployeeDTO addEmployee(AddEmployeeDTO addEmployeeDTO)
        {   
            Employee employee = mapper.Map<Employee>(addEmployeeDTO);
            dbContext.Add(employee);
            dbContext.SaveChanges();
            return mapper.Map<GetEmployeeDTO>(employee);
        }

        public GetEmployeeDTO deleteEmployee(int id)
        {
            Employee employee = dbContext.Employees.Find(id);
            if (employee != null) 
            {
                dbContext.Employees.Remove(employee);
                dbContext.SaveChanges();
                return mapper.Map<GetEmployeeDTO>(employee);
            }
            else
            {
                throw new NotFoundException("Employee doesn't exist");
            }
        }

        public EmployeePaginatedDTO getAllEmployees(int page, int pageSize)
        {
            int total = dbContext.Employees.Count();
            List<Employee> employees = dbContext.Employees.Take(pageSize).ToList();
            List<GetEmployeeDTO> result = new List<GetEmployeeDTO>();
            foreach (Employee employee in employees) {
                result.Add(mapper.Map<GetEmployeeDTO>(employee));
            }
            EmployeePaginatedDTO employeePaginatedDTO = new EmployeePaginatedDTO();
            employeePaginatedDTO.employeeDTOs = result;
            employeePaginatedDTO.count = total;
            return employeePaginatedDTO;
        }

        public GetEmployeeDTO getEmployeeById(int id)
        {
            Employee employee = dbContext.Employees.Find(id);
            if (employee != null) 
            { 
                return mapper.Map<GetEmployeeDTO>(employee);
            }
            else
            {
                throw new NotFoundException("Employee doesn't exist");
            }
        }

        public EmployeePaginatedDTO getEmployeesByPosition(string position, int pageSize)
        {
            var employeesQuery = dbContext.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(position))
            {
                employeesQuery = employeesQuery.Where(e => e.Position.ToLower() == position.ToLower());
            }

            var totalEmployees =  employeesQuery.Count();
            var employees = employeesQuery.Take(pageSize).ToList();

            List<GetEmployeeDTO> result = new List<GetEmployeeDTO>();
            foreach (Employee employee in employees)
            {
                result.Add(mapper.Map<GetEmployeeDTO>(employee));
            }
            EmployeePaginatedDTO employeePaginatedDTO = new EmployeePaginatedDTO();
            employeePaginatedDTO.employeeDTOs = result;
            employeePaginatedDTO.count = totalEmployees;

            return employeePaginatedDTO;
        }

        public GetEmployeeDTO updateEmployee(AddEmployeeDTO updateEmployeeDTO, int id)
        {
            Employee employee = dbContext.Employees.Find(id);
            if (employee != null)
            {
                mapper.Map(updateEmployeeDTO, employee);
                dbContext.Update(employee);
                dbContext.SaveChanges();
                return mapper.Map<GetEmployeeDTO>(employee);
            }
            else
            {
                throw new NotFoundException("Employee doesn't exist");
            }

        }
    }
}
