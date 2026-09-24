using Part3_BuilderPattern;

Invoice invoice = new InvoiceBuilder(1001, "Mahmoud")
    .WithCustomerEmail("mahmoud@example.com")
    .WithBillingAddress(
        "Main Street",
        "Tanta",
        "Gharbia",
        "31511",
        "Egypt")
    .WithShippingAddress(
        "Another Street",
        "Tanta",
        "Gharbia",
        "31511",
        "Egypt")
    .WithOrderInfo(
        DateTime.Now,
        "Credit Card",
        "EGP",
        1000m)
    .WithAdjustments(100m, 50m)
    .Build();

Console.WriteLine($"Invoice ID: {invoice.InvoiceId}");
Console.WriteLine($"Customer: {invoice.CustomerName}");
Console.WriteLine($"Billing City: {invoice.BillingAddress.City}");
Console.WriteLine($"Shipping City: {invoice.ShippingAddress.City}");
Console.WriteLine($"Payment Method: {invoice.PaymentMethod}");
Console.WriteLine($"Subtotal: {invoice.SubTotal}");
Console.WriteLine($"Discount: {invoice.DiscountAmount}");
Console.WriteLine($"Tax: {invoice.TaxAmount}");
Console.WriteLine($"Total: {invoice.TotalAmount}");