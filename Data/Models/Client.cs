using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Client
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }


        public List<Order> Orders { get; set; } = new();
    }
}
