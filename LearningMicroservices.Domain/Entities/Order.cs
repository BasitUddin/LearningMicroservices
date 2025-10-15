using OrderManagement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public Address ShippingAddress { get; private set; }

        private readonly List<OrderItems> _items = new();
        public IReadOnlyCollection<OrderItems> Items => _items.AsReadOnly();

        private Order() { } // EF Core

        public Order(Guid customerId, Address ShippingAddress, List<OrderItems> items)
        {
            CustomerId = customerId;
            ShippingAddress = ShippingAddress ?? throw new ArgumentNullException(nameof(ShippingAddress));
            _items = items;
            OrderDate = DateTime.UtcNow;
            TotalAmount = GetTotal().Amount;
        }

        public void AddItem(Guid productId, int quantity, Money price)
        {
            if (_items.Any(i => i.ProductId == productId))
                throw new InvalidOperationException("Product already added to order.");

            var item = new OrderItems(productId, quantity, price);
            _items.Add(item);
        }

        public Money GetTotal()
        {
            if (!_items.Any()) return new Money(0, "USD");
            return _items
                .Select(i => i.Subtotal)
                .Aggregate((x, y) => x.Add(y));
        }
    }
}
