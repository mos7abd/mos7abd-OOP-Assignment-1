using System;
using System.Collections.Generic;
using System.Text;

namespace Part3_BuilderPattern
{
    // Builds and validates a complete address.
    public class AddressBuilder
    {
        // Required address fields
        private string? _street;
        private string? _city;
        private string? _state;
        private string? _zipCode;
        private string? _country;

        public AddressBuilder WithStreet(string street)
        {
            _street = street;
            return this;
        }

        public AddressBuilder WithCity(string city)
        {
            _city = city;
            return this;
        }

        public AddressBuilder WithState(string state)
        {
            _state = state;
            return this;
        }

        public AddressBuilder WithZipCode(string zipCode)
        {
            _zipCode = zipCode;
            return this;
        }

        public AddressBuilder WithCountry(string country)
        {
            _country = country;
            return this;
        }

        // Validate and create the address.
        public Address Build()
        {
            if (string.IsNullOrWhiteSpace(_street))
                throw new InvalidOperationException("Street is required.");

            if (string.IsNullOrWhiteSpace(_city))
                throw new InvalidOperationException("City is required.");

            if (string.IsNullOrWhiteSpace(_state))
                throw new InvalidOperationException("State is required.");

            if (string.IsNullOrWhiteSpace(_zipCode))
                throw new InvalidOperationException("ZIP code is required.");

            if (string.IsNullOrWhiteSpace(_country))
                throw new InvalidOperationException("Country is required.");

            return new Address
            {
                Street = _street,
                City = _city,
                State = _state,
                ZipCode = _zipCode,
                Country = _country
            };
        }
    }
}
