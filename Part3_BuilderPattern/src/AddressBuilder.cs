using System;
using System.Collections.Generic;
using System.Text;

namespace Part3_BuilderPattern;

public class Address
{
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }
    public string Country { get; }

    public Address(
        string street,
        string city,
        string state,
        string zipCode,
        string country)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
    }
}

public class AddressBuilder
{
    private string _street = string.Empty;
    private string _city = string.Empty;
    private string _state = string.Empty;
    private string _zipCode = string.Empty;
    private string _country = string.Empty;

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

    public Address Build()
    {
        if (string.IsNullOrWhiteSpace(_street) ||
            string.IsNullOrWhiteSpace(_city) ||
            string.IsNullOrWhiteSpace(_state) ||
            string.IsNullOrWhiteSpace(_zipCode) ||
            string.IsNullOrWhiteSpace(_country))
        {
            throw new InvalidOperationException(
                "All address fields are required.");
        }

        return new Address(
            _street,
            _city,
            _state,
            _zipCode,
            _country);
    }
}