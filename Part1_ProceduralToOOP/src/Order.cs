using System;
using System.Collections.Generic;
using System.Text;

namespace Part1_ProceduralToOOP
{
    public class Order
    {
        private readonly List<OrderLine> _lines = new();

        public int Id { get; }
        public Customer Customer { get; }
        public string Date { get; }
        public bool IsPaid { get; private set; }

        public IReadOnlyList<OrderLine> Lines => _lines;

        public Order(int id, Customer customer, string date)
        {
            Id = id;
            Customer = customer;
            Date = date;
            IsPaid = false;
        }

        public void AddLine(Product product, int quantity)
        {
            if (IsPaid)
            {
                throw new InvalidOperationException(
                    "Cannot change a paid order.");
            }

            if (!product.HasEnoughStock(quantity))
            {
                throw new InvalidOperationException(
                    $"Not enough stock for product #{product.Id}.");
            }

            product.ReduceStock(quantity);

            _lines.Add(new OrderLine(product, quantity));
        }

        public double CalculateTotal()
        {
            double total = 0.0;

            foreach (OrderLine line in _lines)
            {
                total += line.GetLineTotal();
            }

            double discount = Customer.GetDiscountRate();

            return total * (1 - discount);
        }

        public void MarkAsPaid()
        {
            if (_lines.Count == 0)
            {
                throw new InvalidOperationException(
                    "Cannot pay an empty order.");
            }

            IsPaid = true;
        }
    }
}
