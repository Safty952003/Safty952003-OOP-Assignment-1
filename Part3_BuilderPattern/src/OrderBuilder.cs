using System;
using System.Collections.Generic;
using System.Text;

namespace Part3_BuilderPattern;

public class OrderInfo
{
    public DateTime OrderDate { get; }
    public string PaymentMethod { get; }
    public string Currency { get; }
    public decimal SubTotal { get; }
    public decimal DiscountAmount { get; }
    public decimal TaxAmount { get; }
    public decimal TotalAmount { get; }

    public OrderInfo(
        DateTime orderDate,
        string paymentMethod,
        string currency,
        decimal subTotal,
        decimal discountAmount,
        decimal taxAmount)
    {
        OrderDate = orderDate;
        PaymentMethod = paymentMethod;
        Currency = currency;
        SubTotal = subTotal;
        DiscountAmount = discountAmount;
        TaxAmount = taxAmount;
        TotalAmount = subTotal - discountAmount + taxAmount;
    }
}

public class OrderBuilder
{
    private DateTime? _orderDate;
    private string? _paymentMethod;
    private string? _currency;
    private decimal? _subTotal;
    private decimal _discountAmount;
    private decimal _taxAmount;

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

    public OrderBuilder WithAdjustments(
        decimal discountAmount = 0,
        decimal taxAmount = 0)
    {
        _discountAmount = discountAmount;
        _taxAmount = taxAmount;
        return this;
    }

    public OrderInfo Build()
    {
        if (!_orderDate.HasValue)
            throw new InvalidOperationException("Order date is required.");

        if (string.IsNullOrWhiteSpace(_paymentMethod))
            throw new InvalidOperationException("Payment method is required.");

        if (string.IsNullOrWhiteSpace(_currency))
            throw new InvalidOperationException("Currency is required.");

        if (!_subTotal.HasValue)
            throw new InvalidOperationException("SubTotal is required.");

        return new OrderInfo(
            _orderDate.Value,
            _paymentMethod,
            _currency,
            _subTotal.Value,
            _discountAmount,
            _taxAmount);
    }
}