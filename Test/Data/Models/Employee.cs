using System.ComponentModel.DataAnnotations;

namespace Core.Data.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set;}
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}
