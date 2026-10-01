using System;
using System.Collections.Generic;
using System.Text;

namespace Part3_BuilderPattern
{
    public class OrderInformation
    {
        public DateTime OrderDate { get; init; }
        public string PaymentMethod { get; init; } = string.Empty;
        public string Currency { get; init; } = string.Empty;

        public decimal SubTotal { get; init; }
        public decimal DiscountAmount { get; init; }
        public decimal TaxAmount { get; init; }
        public decimal TotalAmount { get; init; }
    }
}
