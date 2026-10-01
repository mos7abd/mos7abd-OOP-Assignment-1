namespace Part3_BuilderPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Task 3.2  Solve With a Builder (Test the Builder)

            //Console.WriteLine("=== Valid Invoice ===");

            //var invoice = new InvoiceBuilder()
            //    .WithInvoiceId(1001)
            //    .WithCustomerName("Mostafa Abdella")
            //    .WithCustomerEmail("mostafa@example.com")
            //    .WithCustomerPhone("01000000000")

            //    .WithBillingStreet("Street 1")
            //    .WithBillingCity("Cairo")
            //    .WithBillingState("Cairo")
            //    .WithBillingZipCode("11511")
            //    .WithBillingCountry("Egypt")

            //    .WithShippingStreet("Street 2")
            //    .WithShippingCity("Giza")
            //    .WithShippingState("Giza")
            //    .WithShippingZipCode("12511")
            //    .WithShippingCountry("Egypt")

            //    .WithOrderDate(new DateTime(2026, 10, 1))
            //    .WithPaymentMethod("Credit Card")
            //    .WithCurrency("EGP")
            //    .WithSubTotal(1000)
            //    .WithDiscountAmount(100)
            //    .WithTaxAmount(150)

            //    .Build();

            //Console.WriteLine($"Invoice ID: {invoice.InvoiceId}");
            //Console.WriteLine($"Customer: {invoice.CustomerName}");
            //Console.WriteLine($"Subtotal: {invoice.SubTotal}");
            //Console.WriteLine($"Discount: {invoice.DiscountAmount}");
            //Console.WriteLine($"Tax: {invoice.TaxAmount}");
            //Console.WriteLine($"Total: {invoice.TotalAmount}");

            //Console.WriteLine();
            //Console.WriteLine("=== Invalid Invoice ===");

            //try
            //{
            //    var invalidInvoice = new InvoiceBuilder()
            //        .WithInvoiceId(1002)
            //        .WithCustomerName("Mostafa Abdella")
            //        .Build();
            //}
            //catch (InvalidOperationException ex)
            //{
            //    Console.WriteLine($"Expected error: {ex.Message}");
            //}
            #endregion

            #region Task 3.3 — Refactor Into Smaller, Composed Builders
            // Build the billing address.
            var billingAddress = new AddressBuilder()
                .WithStreet("Billing Street")
                .WithCity("Cairo")
                .WithState("Cairo")
                .WithZipCode("11511")
                .WithCountry("Egypt")
                .Build();

            // Build the shipping address.
            var shippingAddress = new AddressBuilder()
                .WithStreet("Shipping Street")
                .WithCity("Giza")
                .WithState("Giza")
                .WithZipCode("12511")
                .WithCountry("Egypt")
                .Build();

            // Build the order information.
            var order = new OrderBuilder()
                .WithOrderDate(new DateTime(2026, 10, 1))
                .WithPaymentMethod("Credit Card")
                .WithCurrency("EGP")
                .WithSubTotal(1000)
                .WithDiscountAmount(100)
                .WithTaxAmount(150)
                .Build();

            // Build the final invoice.
            var invoice = new InvoiceBuilder()
                .WithInvoiceId(1001)
                .WithCustomerName("Mostafa Abdella")
                .WithCustomerEmail("mostafa@example.com")
                .WithCustomerPhone("01000000000")
                .WithBillingAddress(billingAddress)
                .WithShippingAddress(shippingAddress)
                .WithOrder(order)
                .Build();

            // Display the result.
            Console.WriteLine($"Invoice ID: {invoice.InvoiceId}");
            Console.WriteLine($"Customer: {invoice.CustomerName}");

            Console.WriteLine(
                $"Billing Address: {invoice.BillingAddress.City}, " +
                $"{invoice.BillingAddress.Country}");

            Console.WriteLine(
                $"Shipping Address: {invoice.ShippingAddress.City}, " +
                $"{invoice.ShippingAddress.Country}");

            Console.WriteLine($"Payment Method: {invoice.Order.PaymentMethod}");
            Console.WriteLine($"Subtotal: {invoice.Order.SubTotal}");
            Console.WriteLine($"Discount: {invoice.Order.DiscountAmount}");
            Console.WriteLine($"Tax: {invoice.Order.TaxAmount}");
            Console.WriteLine($"Total: {invoice.Order.TotalAmount}");


            // Test an incomplete address.
            try
            {
                var invalidAddress = new AddressBuilder()
                    .WithStreet("Street 1")
                    .WithCity("Cairo")
                    .Build();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Expected address error: {ex.Message}");
            }

            // Test incomplete order information.
            try
            {
                var invalidOrder = new OrderBuilder()
                    .WithPaymentMethod("Credit Card")
                    .WithCurrency("EGP")
                    .Build();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Expected order error: {ex.Message}");
            }
            #endregion



        }
    }
}
