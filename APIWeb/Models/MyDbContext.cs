using Microsoft.EntityFrameworkCore;

namespace APIWeb.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set;}
    }
}
