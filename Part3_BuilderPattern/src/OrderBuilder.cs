using System;
using System.Collections.Generic;
using System.Text;

namespace Part3_BuilderPattern
{
    public class OrderBuilder
    {
        // Required order information
        private DateTime? _orderDate;
        private string? _paymentMethod;
        private string? _currency;
        private decimal? _subTotal;

        // Optional amounts
        private decimal _discountAmount = 0;
        private decimal _taxAmount = 0;

        public OrderBuilder WithOrderDate(DateTime orderDate)
        {
            _orderDate = orderDate;
            return this;
        }

        public OrderBuilder WithPaymentMethod(string paymentMethod)
        {
            _paymentMethod = paymentMethod;
            return this;
        }

        public OrderBuilder WithCurrency(string currency)
        {
            _currency = currency;
            return this;
        }

        public OrderBuilder WithSubTotal(decimal subTotal)
        {
            _subTotal = subTotal;
            return this;
        }

        public OrderBuilder WithDiscountAmount(decimal discountAmount)
        {
            _discountAmount = discountAmount;
            return this;
        }

        public OrderBuilder WithTaxAmount(decimal taxAmount)
        {
            _taxAmount = taxAmount;
            return this;
        }

        public OrderInformation Build()
        {
            if (_orderDate is null)
                throw new InvalidOperationException("Order date is required.");

            if (string.IsNullOrWhiteSpace(_paymentMethod))
                throw new InvalidOperationException(
                    "Payment method is required.");

            if (string.IsNullOrWhiteSpace(_currency))
                throw new InvalidOperationException(
                    "Currency is required.");

            if (_subTotal is null)
                throw new InvalidOperationException(
                    "Subtotal is required.");

            decimal totalAmount =
                _subTotal.Value
                - _discountAmount
                + _taxAmount;

            return new OrderInformation
            {
                OrderDate = _orderDate.Value,
                PaymentMethod = _paymentMethod,
                Currency = _currency,

                SubTotal = _subTotal.Value,
                DiscountAmount = _discountAmount,
                TaxAmount = _taxAmount,
                TotalAmount = totalAmount
            };
        }
    }
}
