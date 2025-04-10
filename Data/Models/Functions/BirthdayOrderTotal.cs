using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Functions
{
    public class BirthdayOrderTotal
    {
        public Guid ClientId { get; set; }

        public string? FullName { get; set; }

        public DateOnly Birthday { get; set; }

        public decimal Total { get; set; }
    }
}
