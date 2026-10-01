using System;
using System.Collections.Generic;
using System.Text;

namespace Part3_BuilderPattern
{
    #region Task 3.2  Solve With a Builder
    //public class InvoiceBuilder
    //{
    //    // Nullable is used so we can distinguish between "not provided" (null)
    //    // Required customer information
    //    private int? _invoiceId;
    //    private string? _customerName;
    //    private string? _customerEmail;

    //    // Optional customer informationٍ
    //    private string? _customerPhone;

    //    // Required billing address
    //    private string? _billingStreet;
    //    private string? _billingCity;
    //    private string? _billingState;
    //    private string? _billingZipCode;
    //    private string? _billingCountry;

    //    // Required shipping address
    //    private string? _shippingStreet;
    //    private string? _shippingCity;
    //    private string? _shippingState;
    //    private string? _shippingZipCode;
    //    private string? _shippingCountry;

    //    // Required order information
    //    private DateTime? _orderDate;
    //    private string? _paymentMethod;
    //    private string? _currency;
    //    private decimal? _subTotal;

    //    // Optional amounts
    //    private decimal _discountAmount = 0;
    //    private decimal _taxAmount = 0;


    //    // Billing Customer Methods
    //    public InvoiceBuilder WithInvoiceId(int invoiceId)
    //    {
    //        _invoiceId = invoiceId;
    //        return this;
    //    }

    //    public InvoiceBuilder WithCustomerName(string customerName)
    //    {
    //        _customerName = customerName;
    //        return this;
    //    }

    //    public InvoiceBuilder WithCustomerEmail(string customerEmail)
    //    {
    //        _customerEmail = customerEmail;
    //        return this;
    //    }

    //    public InvoiceBuilder WithCustomerPhone(string customerPhone)
    //    {
    //        _customerPhone = customerPhone;
    //        return this;
    //    }


    //    //  Billing Address Methods .
    //    public InvoiceBuilder WithBillingStreet(string street)
    //    {
    //        _billingStreet = street;
    //        return this;
    //    }
    //    public InvoiceBuilder WithBillingCity(string city)
    //    {
    //        _billingCity = city;
    //        return this;
    //    }
    //    public InvoiceBuilder WithBillingState(string state)
    //    {
    //        _billingState = state;
    //        return this;
    //    }
    //    public InvoiceBuilder WithBillingZipCode(string zipCode)
    //    {
    //        _billingZipCode = zipCode;
    //        return this;
    //    }
    //    public InvoiceBuilder WithBillingCountry(string country)
    //    {
    //        _billingCountry = country;
    //        return this;
    //    }



    //    // Shipping Address Methods
    //    public InvoiceBuilder WithShippingStreet(string street)
    //    {
    //        _shippingStreet = street;
    //        return this;
    //    }

    //    public InvoiceBuilder WithShippingCity(string city)
    //    {
    //        _shippingCity = city;
    //        return this;
    //    }

    //    public InvoiceBuilder WithShippingState(string state)
    //    {
    //        _shippingState = state;
    //        return this;
    //    }

    //    public InvoiceBuilder WithShippingZipCode(string zipCode)
    //    {
    //        _shippingZipCode = zipCode;
    //        return this;
    //    }

    //    public InvoiceBuilder WithShippingCountry(string country)
    //    {
    //        _shippingCountry = country;
    //        return this;
    //    }


    //    // Order and Payment Methods
    //    public InvoiceBuilder WithOrderDate(DateTime orderDate)
    //    {
    //        _orderDate = orderDate;
    //        return this;
    //    }
    //    public InvoiceBuilder WithPaymentMethod(string paymentMethod)
    //    {
    //        _paymentMethod = paymentMethod;
    //        return this;
    //    }
    //    public InvoiceBuilder WithCurrency(string currency)
    //    {
    //        _currency = currency;
    //        return this;
    //    }
    //    public InvoiceBuilder WithSubTotal(decimal subTotal)
    //    {
    //        _subTotal = subTotal;
    //        return this;
    //    }


    //    // Optional Amounts
    //    public InvoiceBuilder WithDiscountAmount(decimal discountAmount)
    //    {
    //        _discountAmount = discountAmount;
    //        return this;
    //    }

    //    // Set the optional tax amount.
    //    public InvoiceBuilder WithTaxAmount(decimal taxAmount)
    //    {
    //        _taxAmount = taxAmount;
    //        return this;
    //    }


    //    // Validate the data and create the final Invoice.
    //    public Invoice Build()
    //    {
    //        // Validate required properties.
    //        if (_invoiceId is null)
    //            throw new InvalidOperationException("Invoice ID is required.");

    //        if (string.IsNullOrWhiteSpace(_customerName))
    //            throw new InvalidOperationException("Customer name is required.");

    //        if (string.IsNullOrWhiteSpace(_customerEmail))
    //            throw new InvalidOperationException("Customer email is required.");

    //        if (string.IsNullOrWhiteSpace(_billingStreet))
    //            throw new InvalidOperationException("Billing street is required.");

    //        if (string.IsNullOrWhiteSpace(_billingCity))
    //            throw new InvalidOperationException("Billing city is required.");

    //        if (string.IsNullOrWhiteSpace(_billingState))
    //            throw new InvalidOperationException("Billing state is required.");

    //        if (string.IsNullOrWhiteSpace(_billingZipCode))
    //            throw new InvalidOperationException("Billing ZIP code is required.");

    //        if (string.IsNullOrWhiteSpace(_billingCountry))
    //            throw new InvalidOperationException("Billing country is required.");

    //        if (string.IsNullOrWhiteSpace(_shippingStreet))
    //            throw new InvalidOperationException("Shipping street is required.");

    //        if (string.IsNullOrWhiteSpace(_shippingCity))
    //            throw new InvalidOperationException("Shipping city is required.");

    //        if (string.IsNullOrWhiteSpace(_shippingState))
    //            throw new InvalidOperationException("Shipping state is required.");

    //        if (string.IsNullOrWhiteSpace(_shippingZipCode))
    //            throw new InvalidOperationException("Shipping ZIP code is required.");

    //        if (string.IsNullOrWhiteSpace(_shippingCountry))
    //            throw new InvalidOperationException("Shipping country is required.");

    //        if (_orderDate is null)
    //            throw new InvalidOperationException("Order date is required.");

    //        if (string.IsNullOrWhiteSpace(_paymentMethod))
    //            throw new InvalidOperationException("Payment method is required.");

    //        if (string.IsNullOrWhiteSpace(_currency))
    //            throw new InvalidOperationException("Currency is required.");

    //        if (_subTotal is null)
    //            throw new InvalidOperationException("Subtotal is required.");

    //        // Calculate the total amount.
    //        decimal totalAmount =
    //            _subTotal.Value
    //            - _discountAmount
    //            + _taxAmount;

    //        // Create the final Invoice.
    //        return new Invoice
    //        {
    //            InvoiceId = _invoiceId.Value,

    //            CustomerName = _customerName,
    //            CustomerEmail = _customerEmail,
    //            CustomerPhone = _customerPhone,

    //            BillingStreet = _billingStreet,
    //            BillingCity = _billingCity,
    //            BillingState = _billingState,
    //            BillingZipCode = _billingZipCode,
    //            BillingCountry = _billingCountry,

    //            ShippingStreet = _shippingStreet,
    //            ShippingCity = _shippingCity,
    //            ShippingState = _shippingState,
    //            ShippingZipCode = _shippingZipCode,
    //            ShippingCountry = _shippingCountry,

    //            OrderDate = _orderDate.Value,
    //            PaymentMethod = _paymentMethod,
    //            Currency = _currency,

    //            SubTotal = _subTotal.Value,
    //            DiscountAmount = _discountAmount,
    //            TaxAmount = _taxAmount,
    //            TotalAmount = totalAmount
    //        };
    //    }
    //}
    #endregion


    // Builds the final Invoice from smaller builders.
    public class InvoiceBuilder
    {
        // Customer information
        private int? _invoiceId;
        private string? _customerName;
        private string? _customerEmail;
        private string? _customerPhone;

        // Composed objects
        private Address? _billingAddress;
        private Address? _shippingAddress;
        private OrderInformation? _order;

        // Set the invoice ID.
        public InvoiceBuilder WithInvoiceId(int invoiceId)
        {
            _invoiceId = invoiceId;
            return this;
        }

        // Set the customer name.
        public InvoiceBuilder WithCustomerName(string customerName)
        {
            _customerName = customerName;
            return this;
        }

        // Set the customer email.
        public InvoiceBuilder WithCustomerEmail(string customerEmail)
        {
            _customerEmail = customerEmail;
            return this;
        }

        // Set the optional customer phone.
        public InvoiceBuilder WithCustomerPhone(string customerPhone)
        {
            _customerPhone = customerPhone;
            return this;
        }

        // Set the billing address.
        public InvoiceBuilder WithBillingAddress(Address address)
        {
            _billingAddress = address;
            return this;
        }

        // Set the shipping address.
        public InvoiceBuilder WithShippingAddress(Address address)
        {
            _shippingAddress = address;
            return this;
        }

        // Set the order information.
        public InvoiceBuilder WithOrder(OrderInformation order)
        {
            _order = order;
            return this;
        }

        // Validate and create the final Invoice.
        public Invoice Build()
        {
            // Validate invoice-level required information.
            if (_invoiceId is null)
                throw new InvalidOperationException("Invoice ID is required.");

            if (string.IsNullOrWhiteSpace(_customerName))
                throw new InvalidOperationException("Customer name is required.");

            if (string.IsNullOrWhiteSpace(_customerEmail))
                throw new InvalidOperationException("Customer email is required.");

            if (_billingAddress is null)
                throw new InvalidOperationException(
                    "Billing address is required.");

            if (_shippingAddress is null)
                throw new InvalidOperationException(
                    "Shipping address is required.");

            if (_order is null)
                throw new InvalidOperationException(
                    "Order information is required.");

            // Create the final Invoice.
            return new Invoice
            {
                InvoiceId = _invoiceId.Value,
                CustomerName = _customerName,
                CustomerEmail = _customerEmail,
                CustomerPhone = _customerPhone,
                BillingAddress = _billingAddress,
                ShippingAddress = _shippingAddress,
                Order = _order
            };
        }
    }
}
