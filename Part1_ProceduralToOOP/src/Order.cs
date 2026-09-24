using System;
using System.Collections.Generic;
using System.Text;

namespace Part1_ProceduralToOOP;

public class Order
{
    private readonly List<OrderLine> _lines = new();

    public int Id { get; }
    public Customer Customer { get; }
    public DateTime OrderDate { get; }
    public bool IsPaid { get; private set; }
    public decimal VipDiscountRate { get; }
    public IReadOnlyList<OrderLine> Lines => _lines;

    public Order(
        int id,
        Customer customer,
        DateTime orderDate,
        decimal vipDiscountRate)
    {
        Id = id;
        Customer = customer;
        OrderDate = orderDate;
        VipDiscountRate = vipDiscountRate;
        IsPaid = false;
    }

    public void AddLine(OrderLine line)
    {
        if (IsPaid)
            throw new InvalidOperationException(
                "Cannot change a paid order.");

        _lines.Add(line);
    }

    public decimal CalculateTotal()
    {
        decimal total = 0;

        foreach (OrderLine line in _lines)
        {
            total += (decimal)line.CalculateTotal();
        }

        if (Customer.IsVip)
        {
            total -= total * VipDiscountRate;
        }

        return total;
    }

    public void Pay()
    {
        if (IsPaid)
            throw new InvalidOperationException(
                "Order is already paid.");

        if (_lines.Count == 0)
            throw new InvalidOperationException(
                "Cannot pay an empty order.");

        // Validate stock before changing anything
        foreach (OrderLine line in _lines)
        {
            if (line.Product.Stock < line.Quantity)
            {
                throw new InvalidOperationException(
                    $"Not enough stock for product #{line.Product.Id}.");
            }
        }

        // Decrease stock only after all validation succeeds
        foreach (OrderLine line in _lines)
        {
            line.Product.DecreaseStock(line.Quantity);
        }

        IsPaid = true;
    }
}
