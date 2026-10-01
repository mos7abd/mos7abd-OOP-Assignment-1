using System;
using System.Collections.Generic;
using System.Text;

namespace Part1_ProceduralToOOP
{
    public class OrderSystem
    {
        private readonly List<Customer> _customers = new();
        private readonly List<Product> _products = new();
        private readonly List<Order> _orders = new();

        public IReadOnlyList<Customer> Customers => _customers;
        public IReadOnlyList<Product> Products => _products;
        public IReadOnlyList<Order> Orders => _orders;
        public Customer? FindCustomerById(int id)
        {
            return _customers.FirstOrDefault(
                customer => customer.Id == id);
        }

        public Product? FindProductById(int id)
        {
            return _products.FirstOrDefault(
                product => product.Id == id);
        }

        public Order? FindOrderById(int id)
        {
            return _orders.FirstOrDefault(
                order => order.Id == id);
        }

        public void AddCustomer(int id,string name, string email,string city,bool isVip)
        {
            if (FindCustomerById(id) != null)
            {
                throw new InvalidOperationException(
                    $"Customer id {id} already exists.");
            }

            _customers.Add(
                new Customer(id, name, email, city, isVip));
        }

        public void AddProduct(int id,string name, double price,int stock)
        {
            if (FindProductById(id) != null)
            {
                throw new InvalidOperationException(
                    $"Product id {id} already exists.");
            }

            _products.Add(
                new Product(id, name, price, stock));
        }

        public Order CreateOrder(int orderId,int customerId,string date)
        {
            if (FindOrderById(orderId) != null)
            {
                throw new InvalidOperationException(
                    $"Order id {orderId} already exists.");
            }

            Customer? customer = FindCustomerById(customerId);

            if (customer == null)
            {
                throw new InvalidOperationException(
                    $"Customer id {customerId} not found.");
            }

            Order order = new Order(orderId, customer,date);

            _orders.Add(order);

            return order;

        }

        public double CalculatePaidSalesTotal()
        {
            return _orders
                .Where(order => order.IsPaid)
                .Sum(order => order.CalculateTotal());
        }

        public void AddLineToOrder(
    int orderId,
    int productId,
    int quantity)
        {
            Order? order = FindOrderById(orderId);

            if (order == null)
            {
                throw new InvalidOperationException(
                    $"Order id {orderId} not found.");
            }

            Product? product = FindProductById(productId);

            if (product == null)
            {
                throw new InvalidOperationException(
                    $"Product id {productId} not found.");
            }

            order.AddLine(product, quantity);
        }

        public void SeedSampleData()
        {
            AddCustomer(
                1,
                "Mona Ali",
                "mona@example.com",
                "Cairo",
                true);

            AddCustomer(
                2,
                "Omar Hassan",
                "omar@example.com",
                "Alexandria",
                false);

            AddCustomer(
                3,
                "Sara Nabil",
                "sara@example.com",
                "Giza",
                false);

            AddProduct(
                101,
                "USB Cable",
                50.0,
                100);

            AddProduct(
                102,
                "Wireless Mouse",
                250.0,
                40);

            AddProduct(
                103,
                "Mechanical Keyboard",
                1200.0,
                15);

            AddProduct(
                104,
                "Laptop Stand",
                400.0,
                25);
        }

  

        public void RunDemoScenario()
        {
            CreateOrder(1001, 1, "2026-09-15");
            AddLineToOrder(1001, 101, 2);
            AddLineToOrder(1001, 102, 1);
            FindOrderById(1001)!.MarkAsPaid();

            CreateOrder(1002, 2, "2026-09-15");
            AddLineToOrder(1002, 103, 1);
            AddLineToOrder(1002, 104, 1);

            CreateOrder(1003, 3, "2026-09-16");
            AddLineToOrder(1003, 101, 5);
            FindOrderById(1003)!.MarkAsPaid();
        }
    }
}
