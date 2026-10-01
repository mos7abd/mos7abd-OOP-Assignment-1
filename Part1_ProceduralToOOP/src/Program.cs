namespace Part1_ProceduralToOOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OrderSystem system = new OrderSystem();
            system.SeedSampleData();
            system.RunDemoScenario();

            Console.WriteLine("===== ORDER SYSTEM =====");

            RunInteractiveMenu(system);
        }
        private static void RunInteractiveMenu(OrderSystem system)
        {
            while (true)
            {
                PrintMenu();

                string? choice = Console.ReadLine();

                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        PrintCustomers(system);
                        break;

                    case "2":
                        PrintProducts(system);
                        break;

                    case "3":
                        PrintAllOrders(system);
                        break;

                    case "4":
                        PrintOneOrder(system);
                        break;

                    case "5":
                        CreateOrder(system);
                        break;

                    case "6":
                        AddLineToOrder(system);
                        break;

                    case "7":
                        MarkOrderPaid(system);
                        break;

                    case "8":
                        ShowPaidSalesTotal(system);
                        break;

                    case "0":
                        Console.WriteLine("Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

                Console.WriteLine();
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine("\n---------- MENU ----------");
            Console.WriteLine("1) Print customers");
            Console.WriteLine("2) Print products");
            Console.WriteLine("3) Print all orders");
            Console.WriteLine("4) Print one order by id");
            Console.WriteLine("5) Create order");
            Console.WriteLine("6) Add line to order");
            Console.WriteLine("7) Mark order paid");
            Console.WriteLine("8) Show paid sales total");
            Console.WriteLine("0) Exit");
            Console.Write("Choice: ");
        }

        private static void PrintCustomers(OrderSystem system)
        {
            Console.WriteLine("---------- CUSTOMERS ----------");

            if (system.Customers.Count == 0)
            {
                Console.WriteLine("No customers found.");
                return;
            }

            foreach (Customer customer in system.Customers)
            {
                Console.WriteLine(
                    $"ID: {customer.Id} | " +
                    $"Name: {customer.Name} | " +
                    $"Email: {customer.Email} | " +
                    $"City: {customer.City} | " +
                    $"VIP: {customer.IsVip}");
            }
        }

        private static void PrintProducts(OrderSystem system)
        {
            Console.WriteLine("---------- PRODUCTS ----------");

            if (system.Products.Count == 0)
            {
                Console.WriteLine("No products found.");
                return;
            }

            foreach (Product product in system.Products)
            {
                Console.WriteLine(
                    $"ID: {product.Id} | " +
                    $"Name: {product.Name} | " +
                    $"Price: {product.Price:F2} | " +
                    $"Stock: {product.Stock}");
            }
        }

        private static void PrintAllOrders(OrderSystem system)
        {
            Console.WriteLine("---------- ORDERS ----------");

            if (system.Orders.Count == 0)
            {
                Console.WriteLine("No orders found.");
                return;
            }

            foreach (Order order in system.Orders)
            {
                PrintOrderDetails(order);
            }
        }

        private static void PrintOneOrder(OrderSystem system)
        {
            Console.Write("Enter order id: ");

            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.WriteLine("Invalid order id.");
                return;
            }

            Order? order = system.FindOrderById(orderId);

            if (order == null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            PrintOrderDetails(order);
        }

        private static void PrintOrderDetails(Order order)
        {
            Console.WriteLine(
                $"Order ID: {order.Id} | " +
                $"Customer: {order.Customer.Name} | " +
                $"Date: {order.Date} | " +
                $"Paid: {order.IsPaid}");

            Console.WriteLine("Lines:");

            if (order.Lines.Count == 0)
            {
                Console.WriteLine("  No lines.");
            }
            else
            {
                foreach (OrderLine line in order.Lines)
                {
                    Console.WriteLine(
                        $"  Product: {line.Product.Name} | " +
                        $"Quantity: {line.Quantity} | " +
                        $"Line Total: {line.GetLineTotal():F2}");
                }
            }

            Console.WriteLine(
                $"Total: {order.CalculateTotal():F2}");

            Console.WriteLine("----------------------------");
        }

        private static void CreateOrder(OrderSystem system)
        {
            Console.Write("Enter order id: ");

            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.WriteLine("Invalid order id.");
                return;
            }

            Console.Write("Enter customer id: ");

            if (!int.TryParse(Console.ReadLine(), out int customerId))
            {
                Console.WriteLine("Invalid customer id.");
                return;
            }

            Console.Write("Enter order date: ");
            string? date = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(date))
            {
                Console.WriteLine("Invalid date.");
                return;
            }

            try
            {
                Order order = system.CreateOrder(
                    orderId,
                    customerId,
                    date);

                Console.WriteLine(
                    $"Order #{order.Id} created successfully.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void AddLineToOrder(OrderSystem system)
        {
            Console.Write("Enter order id: ");

            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.WriteLine("Invalid order id.");
                return;
            }

            Order? order = system.FindOrderById(orderId);

            if (order == null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            Console.Write("Enter product id: ");

            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("Invalid product id.");
                return;
            }

            Product? product = system.FindProductById(productId);

            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.Write("Enter quantity: ");

            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid quantity.");
                return;
            }

            try
            {
                order.AddLine(product, quantity);

                Console.WriteLine(
                    $"Product #{product.Id} added to order #{order.Id}.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void MarkOrderPaid(OrderSystem system)
        {
            Console.Write("Enter order id: ");

            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.WriteLine("Invalid order id.");
                return;
            }

            Order? order = system.FindOrderById(orderId);

            if (order == null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            try
            {
                order.MarkAsPaid();

                Console.WriteLine(
                    $"Order #{order.Id} marked as paid.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ShowPaidSalesTotal(OrderSystem system)
        {
            double total = system.CalculatePaidSalesTotal();

            Console.WriteLine(
                $"Paid sales total: {total:F2}");
        }
    }
}
