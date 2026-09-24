using System;
using System.Collections.Generic;
using System.Text;

namespace Part3_BuilderPattern;

public class InvoiceBuilder
{
    private readonly int _invoiceId;
    private readonly string _customerName;

    private string? _customerEmail;

    private readonly AddressBuilder _billingAddressBuilder = new();
    private readonly AddressBuilder _shippingAddressBuilder = new();
    private readonly OrderBuilder _orderBuilder = new();

    public InvoiceBuilder(int invoiceId, string customerName)
    {
        if (string.IsNullOrWhiteSpace(customerName))
            throw new ArgumentException("Customer name is required.");

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
        _billingAddressBuilder
            .WithStreet(street)
            .WithCity(city)
            .WithState(state)
            .WithZipCode(zipCode)
            .WithCountry(country);

        return this;
    }

    public InvoiceBuilder WithShippingAddress(
        string street,
        string city,
        string state,
        string zipCode,
        string country)
    {
        _shippingAddressBuilder
            .WithStreet(street)
            .WithCity(city)
            .WithState(state)
            .WithZipCode(zipCode)
            .WithCountry(country);

        return this;
    }

    public InvoiceBuilder WithOrderInfo(
        DateTime orderDate,
        string paymentMethod,
        string currency,
        decimal subTotal)
    {
        _orderBuilder
            .WithOrderDate(orderDate)
            .WithPaymentMethod(paymentMethod)
            .WithCurrency(currency)
            .WithSubTotal(subTotal);

        return this;
    }

    public InvoiceBuilder WithAdjustments(
        decimal discountAmount = 0,
        decimal taxAmount = 0)
    {
        _orderBuilder.WithAdjustments(
            discountAmount,
            taxAmount);

        return this;
    }

    public Invoice Build()
    {
        Address billingAddress =
            _billingAddressBuilder.Build();

        Address shippingAddress =
            _shippingAddressBuilder.Build();

        OrderInfo orderInfo =
            _orderBuilder.Build();

        return new Invoice(
            _invoiceId,
            _customerName,
            _customerEmail ?? string.Empty,
            billingAddress,
            shippingAddress,
            orderInfo.OrderDate,
            orderInfo.PaymentMethod,
            orderInfo.Currency,
            orderInfo.SubTotal,
            orderInfo.DiscountAmount,
            orderInfo.TaxAmount,
            orderInfo.TotalAmount);
    }
}