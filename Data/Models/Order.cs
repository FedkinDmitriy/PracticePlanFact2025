using Data.Models.Enums;

namespace Data.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public decimal OrderSum { get; set; }

        public DateTime OrdersDateTime { get; set; }

        public OrderStatus Status { get; set; }


        public Guid ClientId { get; set; }

        public Client? Client { get; set; }
    }
}
