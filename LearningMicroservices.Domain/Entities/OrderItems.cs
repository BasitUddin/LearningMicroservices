using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Entities
{
    public class OrderItems
    {
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public Money UnitPrice { get; private set; }
        [JsonIgnore]
        public Money Subtotal => UnitPrice.Multiply(Quantity);

        private OrderItems() { }

        public OrderItems(Guid productId, int quantity, Money price)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = price;
        }
    }
}
