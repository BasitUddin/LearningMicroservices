using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Entities
{
    public class Money 
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }

        private Money() { }

        public Money(decimal amount, string currency)
        {
            if (amount < 0) throw new ArgumentException("Amount cannot be negative.");
            Amount = amount;
            Currency = currency;
        }

        public Money Add(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Currency mismatch");

            return new Money(Amount + other.Amount, Currency);
        }

        public Money Multiply(int quantity)
            => new Money(Amount * quantity, Currency);
    }
}
