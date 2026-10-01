using System;
using System.Collections.Generic;
using System.Text;

namespace Part3_BuilderPattern
{
    // Represents a complete address.
    public class Address
    {
        public string Street { get; init; } = string.Empty;
        public string City { get; init; } = string.Empty;
        public string State { get; init; } = string.Empty;
        public string ZipCode { get; init; } = string.Empty;
        public string Country { get; init; } = string.Empty;
    }
}
