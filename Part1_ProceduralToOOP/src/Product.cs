using System;
using System.Collections.Generic;
using System.Text;

namespace Part1_ProceduralToOOP;

public class Product
{
    public int Id { get; }
    public string Name { get; }
    public decimal Price { get; }
    public int Stock { get; private set; }

    public Product(int id, string name, decimal price, int stock)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty.");

        if (price < 0)
            throw new ArgumentException("Price cannot be negative.");

        if (stock < 0)
            throw new ArgumentException("Stock cannot be negative.");

        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.");

        if (quantity > Stock)
            throw new InvalidOperationException("Not enough stock.");

        Stock -= quantity;
    }
}
