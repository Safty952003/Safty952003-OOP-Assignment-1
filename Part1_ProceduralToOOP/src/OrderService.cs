using System;
using System.Collections.Generic;
using System.Text;

namespace Part1_ProceduralToOOP;

public class OrderService
{
    private readonly List<Customer> _customers;
    private readonly List<Product> _products;
    private readonly List<Order> _orders;

    public decimal VipDiscountRate { get; }

    public IReadOnlyList<Customer> Customers => _customers;
    public IReadOnlyList<Product> Products => _products;
    public IReadOnlyList<Order> Orders => _orders;

    public OrderService(decimal vipDiscountRate)
    {
        VipDiscountRate = vipDiscountRate;

        _customers = new List<Customer>
        {
            new Customer(
                1,
                "Mona Ali",
                "mona@example.com",
                "Cairo",
                true),

            new Customer(
                2,
                "Omar Hassan",
                "omar@example.com",
                "Alexandria",
                false),

            new Customer(
                3,
                "Sara Nabil",
                "sara@example.com",
                "Giza",
                false)
        };

        _products = new List<Product>
        {
            new Product(101, "USB Cable", 50m, 100),
            new Product(102, "Wireless Mouse", 250m, 40),
            new Product(103, "Mechanical Keyboard", 1200m, 15),
            new Product(104, "Laptop Stand", 400m, 25)
        };

        _orders = new List<Order>();
    }

    public Order CreateOrder(
        int orderId,
        int customerId,
        DateTime orderDate)
    {
        if (FindOrderById(orderId) != null)
        {
            throw new InvalidOperationException(
                $"Order ID {orderId} already exists.");
        }

        Customer? customer = FindCustomerById(customerId);

        if (customer == null)
        {
            throw new InvalidOperationException(
                $"Customer ID {customerId} not found.");
        }

        Order order = new Order(
            orderId,
            customer,
            orderDate,
            VipDiscountRate);

        _orders.Add(order);

        return order;
    }

    public void AddLineToOrder(
        int orderId,
        int productId,
        int quantity)
    {
        Order? order = FindOrderById(orderId);

        if (order == null)
        {
            throw new InvalidOperationException(
                $"Order ID {orderId} not found.");
        }

        Product? product = FindProductById(productId);

        if (product == null)
        {
            throw new InvalidOperationException(
                $"Product ID {productId} not found.");
        }

        OrderLine line = new OrderLine(product, quantity);

        order.AddLine(line);
    }

    public void PayOrder(int orderId)
    {
        Order? order = FindOrderById(orderId);

        if (order == null)
        {
            throw new InvalidOperationException(
                $"Order ID {orderId} not found.");
        }

        order.Pay();
    }

    public decimal GetPaidSalesTotal()
    {
        decimal total = 0;

        foreach (Order order in _orders)
        {
            if (order.IsPaid)
            {
                total += order.CalculateTotal();
            }
        }

        return total;
    }

    public Customer? FindCustomerById(int id)
    {
        return _customers.FirstOrDefault(
            customer => customer.Id == id);
    }

    public Product? FindProductById(int id)
    {
        return _products.FirstOrDefault(
            product => product.Id == id);
    }

    public Order? FindOrderById(int id)
    {
        return _orders.FirstOrDefault(
            order => order.Id == id);
    }
}