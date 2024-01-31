using System.ComponentModel.DataAnnotations;

namespace APIWeb
{
    public class Employee
    {

        [Key]
        public Guid IdEmployee { get; set;}
        public string? FirstName { get; set; }
        public string? LastName { get; set;}
        public string? Email { get; set;}
        public string? Phone { get; set;}


    }
}
