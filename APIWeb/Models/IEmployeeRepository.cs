namespace APIWeb.Models
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetById(Guid id);
        Task<Employee> GetByEmail(string email);
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> AddEmployee(Employee employe);
        Task DeleteEmployee(Guid id);
        Task<Employee> UpdateEmployee(Employee employee);





    }
}
