using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Part3_BuilderPattern;

public class Invoice
{
    public int InvoiceId { get; }
    public string CustomerName { get; }
    public string CustomerEmail { get; }

    public Address BillingAddress { get; }
    public Address ShippingAddress { get; }

    public DateTime OrderDate { get; }
    public string PaymentMethod { get; }
    public string Currency { get; }
    public decimal SubTotal { get; }
    public decimal DiscountAmount { get; }
    public decimal TaxAmount { get; }
    public decimal TotalAmount { get; }

    public Invoice(
        int invoiceId,
        string customerName,
        string customerEmail,
       Address billingAddress,
       Address shippingAddress,
        DateTime orderDate,
        string paymentMethod,
        string currency,
        decimal subTotal,
        decimal discountAmount,
        decimal taxAmount,
        decimal totalAmount)
    {
        InvoiceId = invoiceId;
        CustomerName = customerName;
        CustomerEmail = customerEmail;

        BillingAddress = billingAddress;
        ShippingAddress = shippingAddress;

        OrderDate = orderDate;
        PaymentMethod = paymentMethod;
        Currency = currency;
        SubTotal = subTotal;
        DiscountAmount = discountAmount;
        TaxAmount = taxAmount;
        TotalAmount = totalAmount;
    }
}
