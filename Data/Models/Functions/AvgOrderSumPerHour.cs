using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Functions
{
    public class AvgOrderSumPerHour
    {
        public int Hour { get; set; }

        public decimal AvgSum { get; set; }
    }
}
