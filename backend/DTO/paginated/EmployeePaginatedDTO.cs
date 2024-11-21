using backend.DTO.response;

namespace backend.DTO.paginated
{
    public class EmployeePaginatedDTO
    {
        public List<GetEmployeeDTO> employeeDTOs { get; set; }
        public int count {  get; set; }
    }
}
