using Part1_ProceduralToOOP;

OrderService service = new OrderService(vipDiscountRate: 0.10m);

bool running = true;

while (running)
{
    Console.WriteLine();
    Console.WriteLine("---------- MENU ----------");
    Console.WriteLine("1) Print customers");
    Console.WriteLine("2) Print products");
    Console.WriteLine("3) Print all orders");
    Console.WriteLine("4) Print one order by id");
    Console.WriteLine("5) Create order");
    Console.WriteLine("6) Add line to order");
    Console.WriteLine("7) Mark order paid");
    Console.WriteLine("8) Show paid sales total");
    Console.WriteLine("0) Exit");
    Console.Write("Choose an option: ");

    string? input = Console.ReadLine();
    Console.WriteLine();

    try
    {
        switch (input)
        {
            case "1":
                service.PrintCustomers();
                break;

            case "2":
                service.PrintProducts();
                break;

            case "3":
                service.PrintAllOrders();
                break;

            case "4":
            {
                int orderId = ReadInt("Enter order id: ");
                service.PrintOrderById(orderId);
                break;
            }

            case "5":
            {
                int orderId = ReadInt("Enter new order id: ");
                int customerId = ReadInt("Enter customer id: ");
                DateTime orderDate = ReadDate("Enter order date (yyyy-mm-dd): ");

                Order order = service.CreateOrder(orderId, customerId, orderDate);
                Console.WriteLine($"Order #{order.Id} created.");
                break;
            }

            case "6":
            {
                int orderId = ReadInt("Enter order id: ");
                int productId = ReadInt("Enter product id: ");
                int quantity = ReadInt("Enter quantity: ");

                service.AddLineToOrder(orderId, productId, quantity);
                Console.WriteLine("Line added.");
                break;
            }

            case "7":
            {
                int orderId = ReadInt("Enter order id: ");
                service.PayOrder(orderId);
                Console.WriteLine("Order marked as paid.");
                break;
            }

            case "8":
                Console.WriteLine($"Paid sales total: {service.GetPaidSalesTotal():C}");
                break;

            case "0":
                running = false;
                break;

            default:
                Console.WriteLine("Unknown option. Please try again.");
                break;
        }
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

static int ReadInt(string prompt)
{
    while (true)
    {
        Console.Write(prompt);

        if (int.TryParse(Console.ReadLine(), out int value))
        {
            return value;
        }

        Console.WriteLine("Please enter a valid whole number.");
    }
}

static DateTime ReadDate(string prompt)
{
    while (true)
    {
        Console.Write(prompt);

        if (DateTime.TryParse(Console.ReadLine(), out DateTime value))
        {
            return value;
        }

        Console.WriteLine("Please enter a valid date (e.g. 2025-01-31).");
    }
}
