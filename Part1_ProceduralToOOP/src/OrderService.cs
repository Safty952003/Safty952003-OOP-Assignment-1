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

    public void PrintCustomers()
    {
        if (_customers.Count == 0)
        {
            Console.WriteLine("No customers.");
            return;
        }

        foreach (Customer customer in _customers)
        {
            string type = customer.IsVip ? "VIP" : "Regular";

            Console.WriteLine(
                $"#{customer.Id} | {customer.Name} | {customer.Email} | {customer.City} | {type}");
        }
    }

    public void PrintProducts()
    {
        if (_products.Count == 0)
        {
            Console.WriteLine("No products.");
            return;
        }

        foreach (Product product in _products)
        {
            Console.WriteLine(
                $"#{product.Id} | {product.Name} | Price: {product.Price:C} | Stock: {product.Stock}");
        }
    }

    public void PrintAllOrders()
    {
        if (_orders.Count == 0)
        {
            Console.WriteLine("No orders yet.");
            return;
        }

        foreach (Order order in _orders)
        {
            PrintOrder(order);
            Console.WriteLine(new string('-', 40));
        }
    }

    public void PrintOrderById(int orderId)
    {
        Order? order = FindOrderById(orderId);

        if (order == null)
        {
            throw new InvalidOperationException(
                $"Order ID {orderId} not found.");
        }

        PrintOrder(order);
    }

    private static void PrintOrder(Order order)
    {
        string paid = order.IsPaid ? "Yes" : "No";
        string vip = order.Customer.IsVip ? " (VIP)" : "";

        Console.WriteLine(
            $"Order #{order.Id} | Date: {order.OrderDate:d} | Customer: {order.Customer.Name}{vip} | Paid: {paid}");

        if (order.Lines.Count == 0)
        {
            Console.WriteLine("  (no lines)");
        }
        else
        {
            foreach (OrderLine line in order.Lines)
            {
                Console.WriteLine(
                    $"  {line.Product.Name} x{line.Quantity} @ {line.Product.Price:C} = {line.CalculateTotal():C}");
            }
        }

        Console.WriteLine($"  Total: {order.CalculateTotal():C}");
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