using System;
using System.Collections.Generic;
using System.Text;

namespace Part3_BuilderPattern
{


    #region Task 3.2  Solve With a Builder
    //public class Invoice
    //{
    //    // Customer information
    //    public int InvoiceId { get; init; }
    //    public string CustomerName { get; init; } = string.Empty;
    //    public string CustomerEmail { get; init; } = string.Empty;
    //    public string? CustomerPhone { get; init; }

    //    // Billing address
    //    public string BillingStreet { get; init; } = string.Empty;
    //    public string BillingCity { get; init; } = string.Empty;
    //    public string BillingState { get; init; } = string.Empty;
    //    public string BillingZipCode { get; init; } = string.Empty;
    //    public string BillingCountry { get; init; } = string.Empty;

    //    // Shipping address
    //    public string ShippingStreet { get; init; } = string.Empty;
    //    public string ShippingCity { get; init; } = string.Empty;
    //    public string ShippingState { get; init; } = string.Empty;
    //    public string ShippingZipCode { get; init; } = string.Empty;
    //    public string ShippingCountry { get; init; } = string.Empty;

    //    // Order and payment information
    //    public DateTime OrderDate { get; init; }
    //    public string PaymentMethod { get; init; } = string.Empty;
    //    public string Currency { get; init; } = string.Empty;

    //    // Amount information
    //    public decimal SubTotal { get; init; }
    //    public decimal DiscountAmount { get; init; }
    //    public decimal TaxAmount { get; init; }
    //    public decimal TotalAmount { get; init; }
    //}

    // We use 'init' instead of 'set' because the Invoice should be immutable after it is created.
    // The Builder is responsible for setting the properties during construction,
    // and their values cannot be changed arbitrarily after the Invoice is created.
    #endregion

    // Represents the final invoice object.
    public class Invoice
    {
        // Customer information
        public int InvoiceId { get; init; }
        public string CustomerName { get; init; } = string.Empty;
        public string CustomerEmail { get; init; } = string.Empty;
        public string? CustomerPhone { get; init; }

        // Address information
        public Address BillingAddress { get; init; } = null!;
        public Address ShippingAddress { get; init; } = null!;

        // Order and payment information
        public OrderInformation Order { get; init; } = null!;
    }



}
