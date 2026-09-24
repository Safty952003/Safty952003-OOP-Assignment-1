using System;
using System.Collections.Generic;
using System.Text;

namespace Part3_BuilderPattern;

public class InvoiceBuilder
{
    private readonly int _invoiceId;
    private readonly string _customerName;

    private string? _customerEmail;

    private string? _billingStreet;
    private string? _billingCity;
    private string? _billingState;
    private string? _billingZipCode;
    private string? _billingCountry;

    private string? _shippingStreet;
    private string? _shippingCity;
    private string? _shippingState;
    private string? _shippingZipCode;
    private string? _shippingCountry;

    private DateTime? _orderDate;
    private string? _paymentMethod;
    private string? _currency;
    private decimal? _subTotal;
    private decimal _discountAmount;
    private decimal _taxAmount;

    public InvoiceBuilder(int invoiceId, string customerName)
    {
        if (string.IsNullOrWhiteSpace(customerName))
            throw new ArgumentException(
                "Customer name is required.");

        _invoiceId = invoiceId;
        _customerName = customerName;
    }

    public InvoiceBuilder WithCustomerEmail(string customerEmail)
    {
        _customerEmail = customerEmail;
        return this;
    }

    public InvoiceBuilder WithBillingAddress(
        string street,
        string city,
        string state,
        string zipCode,
        string country)
    {
        _billingStreet = street;
        _billingCity = city;
        _billingState = state;
        _billingZipCode = zipCode;
        _billingCountry = country;

        return this;
    }

    public InvoiceBuilder WithShippingAddress(
        string street,
        string city,
        string state,
        string zipCode,
        string country)
    {
        _shippingStreet = street;
        _shippingCity = city;
        _shippingState = state;
        _shippingZipCode = zipCode;
        _shippingCountry = country;

        return this;
    }

    public InvoiceBuilder WithOrderInfo(
        DateTime orderDate,
        string paymentMethod,
        string currency,
        decimal subTotal)
    {
        _orderDate = orderDate;
        _paymentMethod = paymentMethod;
        _currency = currency;
        _subTotal = subTotal;

        return this;
    }

    public InvoiceBuilder WithAdjustments(
        decimal discountAmount = 0,
        decimal taxAmount = 0)
    {
        _discountAmount = discountAmount;
        _taxAmount = taxAmount;

        return this;
    }

    public Invoice Build()
    {
        decimal subTotal = _subTotal ?? 0;

        decimal totalAmount =
            subTotal - _discountAmount + _taxAmount;

        return new Invoice(
            _invoiceId,
            _customerName,
            _customerEmail ?? string.Empty,

            _billingStreet ?? string.Empty,
            _billingCity ?? string.Empty,
            _billingState ?? string.Empty,
            _billingZipCode ?? string.Empty,
            _billingCountry ?? string.Empty,

            _shippingStreet ?? string.Empty,
            _shippingCity ?? string.Empty,
            _shippingState ?? string.Empty,
            _shippingZipCode ?? string.Empty,
            _shippingCountry ?? string.Empty,

            _orderDate ?? DateTime.MinValue,
            _paymentMethod ?? string.Empty,
            _currency ?? string.Empty,
            subTotal,
            _discountAmount,
            _taxAmount,
            totalAmount);
    }
}