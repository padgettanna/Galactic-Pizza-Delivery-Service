using System.ComponentModel.Design;
using System.Data.SqlTypes;
using System.Net;

namespace Galactic_Pizza_Delivery_Service;

class Program
{
    public static Dictionary<string, int> menu = new Dictionary<string, int>()
    {
        {"Galactic Cheese", 10 },
        {"Meteor Meat Lover", 15 },
        {"Veggie Nebula", 12 },
        {"Black Hole BBQ", 18 }
    };

    public static Dictionary<string, int> deliveryFees = new Dictionary<string, int>()
    {
        {"earth", 5 },
        {"mars", 10 },
        {"jupiter station", 15 },
        {"venus outpost", 8 }
    };
    static void Main(string[] args)
    {
        string customerName = "";
        string customerPlanet = "";
        int totalCost = 0;
        int deliveryFee = 0;
        bool wantsToAddMore = true;

        Console.WriteLine("Welcome to the Galactic Pizze Delivery Service!");
        Console.WriteLine("Please enter your name:");

        while (string.IsNullOrWhiteSpace(customerName))
        {
            customerName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(customerName))
            {
                Console.WriteLine("Name cannot be empty. Please enter your name:");
            }
        }

        Console.WriteLine("Please enter your location:");
        customerPlanet = GetCustomerLocation();

        DisplayMenu();
        Dictionary<string, int> orderList = GetCustomerOrder();
        deliveryFee = GetDeliveryFee(customerPlanet);
        PrintReceipt(customerName, customerPlanet, totalCost, deliveryFee);

    }

    public static string GetCustomerLocation()
    {
        string customerLocation = "";

        while (!deliveryFees.ContainsKey(customerLocation))
        {
            customerLocation = Console.ReadLine().ToLower();

            if (!deliveryFees.ContainsKey(customerLocation))
            {
                Console.WriteLine($"Sorry, we do not deliver to {customerLocation}. Please select Earth, Mars, Jupiter Station, or Venus Outpost.");
            }
        }
        return customerLocation;
    }

    public static void DisplayMenu()
    {
        Console.WriteLine("Galactic Pizza Menu".PadLeft(30, '-').PadRight(40, '-'));
        int index = 1;
        foreach (KeyValuePair<string, int> item in menu)
        {
            Console.WriteLine($"[{index}] {item.Key.PadRight(25)} {item.Value} credits");
            index++;
        }
        Console.WriteLine("".PadLeft(40, '-'));
        Console.WriteLine("Please select the number of pizza from the menu:");
    }
    public static Dictionary<string, int> GetCustomerOrder()
    {
        string orderItem = "";
        string[] validOrders = { "1", "2", "3", "4" };
        Dictionary<string, int> orderList = new Dictionary<string, int>();
        string addMoreResponse = "";
        bool isValidOrder = false;
        bool wantsToAddMore = true;

        DisplayMenu();
        do
        {
            do
            {
                orderItem = Console.ReadLine();

                if (!validOrders.Contains(orderItem))
                {
                    Console.WriteLine("Invalid input. Please select a menu item (1, 2, 3, or 4).");
                }
                else
                {
                    isValidOrder = true;
                }
            } while (!isValidOrder);

            orderList.Add(menu.ElementAt(int.Parse(orderItem) - 1).Key, menu.ElementAt(int.Parse(orderItem) - 1).Value);

            Console.WriteLine("Current Order:".PadLeft(30, '-').PadRight(40, '-'));
            foreach (KeyValuePair<string, int> item in orderList)
            {
                Console.WriteLine($"{item.Key.PadRight(25)} @ {item.Value}credits");
            }

            Console.WriteLine("Would you like to add another pizza to your order? (y/n)");
            while (addMoreResponse != "yes" && addMoreResponse != "no")
            {
                addMoreResponse = Console.ReadLine().ToLower();
                Console.WriteLine($"you selected {addMoreResponse}");
            }
            if (addMoreResponse == "no")
            {
                wantsToAddMore = false;
            }
        } while (wantsToAddMore);

        return orderList;
    }

    public static int GetDeliveryFee(string location)
    {
        int deliveryFee = 0;

        switch (location)
        {
            case "earth":
                deliveryFee += 5;
                break;
            case "mars":
                deliveryFee += 10;
                break;
            case "jupiter station":
                deliveryFee += 15;
                break;
            case "black hole bbq":
                deliveryFee += 8;
                break;
            default:
                break;
        }
        return deliveryFee;
    }
    public static void PrintReceipt(string customerName, string customerPlanet, int totalCost, int deliveryFee)
    {
        Console.WriteLine($"Order for {customerName} to {customerPlanet}");
        Console.WriteLine("".PadLeft(40, '-'));
        Console.WriteLine($"Subtotal: {totalCost} credits.");
        Console.WriteLine($"Delivery fee: {deliveryFee} credits");
        Console.WriteLine($"Total Due: {totalCost + deliveryFee} credits.");
    }

}
