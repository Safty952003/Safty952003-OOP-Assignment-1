using System;
using System.Collections.Generic;
using System.Text;

namespace Part1_ProceduralToOOP;

public class OrderLine
{
    public Product Product { get; }
    public int Quantity { get; }

    public OrderLine(Product product, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.");

        Product = product;
        Quantity = quantity;
    }

    public double CalculateTotal()
    {
        return Product.Price * Quantity;
    }
}