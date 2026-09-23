using System;
using System.Collections.Generic;
using System.Text;

namespace Part1_ProceduralToOOP;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public bool IsVip { get; set; }

    public Customer(int id, string name, string email, string city, bool isVip)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Customer name cannot be empty.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Customer email cannot be empty.");

        Id = id;
        Name = name;
        Email = email;
        City = city;
        IsVip = isVip;
    }
}
