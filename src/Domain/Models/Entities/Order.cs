using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public Guid SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
        public DateOnly OrderDate { get; set; }
        public DateOnly? DeliveryDate { get; set; }
        public int DayNumber { get; set; }
        public bool IsCompleted { get; set; }
        public ICollection<OrderMeal> Meals { get; set; } = new List<OrderMeal>();
    }
}
