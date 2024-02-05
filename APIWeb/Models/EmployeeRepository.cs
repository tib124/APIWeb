
using Microsoft.EntityFrameworkCore;

namespace APIWeb.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyDbContext _Db;

        public EmployeeRepository(MyDbContext db)
        {
            _Db = db;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var employeetoadd = await _Db.Employees.AddAsync(employee);
            await _Db.SaveChangesAsync();

            return employeetoadd.Entity;
        }

        public async Task DeleteEmployee(Guid id)
        {
            var employeetodelete = await _Db.Employees.FindAsync(id);

            if(employeetodelete != null)
            {
                _Db.Employees.Remove(employeetodelete);
                _Db.SaveChangesAsync();

            }
        }

        public async Task<IEnumerable<Employee>> GetAll() => await _Db.Employees.ToListAsync();


        public async Task<Employee> GetByEmail(string email) => await _Db.Employees.FindAsync(email);



        public async Task<Employee> GetById(Guid id) => await _Db.Employees.FindAsync(id);


        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var EmployeeToUpdate = await _Db.Employees.FirstOrDefaultAsync(s => s.IdEmployee == employee.IdEmployee);

            if (EmployeeToUpdate != null)
            {
                EmployeeToUpdate.FirstName = employee.FirstName;
                EmployeeToUpdate.LastName = employee.LastName;
                EmployeeToUpdate.Email = employee.Email;
                EmployeeToUpdate.Phone = employee.Phone;
                await _Db.SaveChangesAsync();

            }
            return EmployeeToUpdate;
        }
    }
}

