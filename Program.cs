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
        double discount = 0;
        int deliveryFee = 0;
        Dictionary<string, int> orderList = new Dictionary<string, int>();

        Console.WriteLine("Welcome to the Galactic Pizza Delivery Service!");
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

        do
        {
            DisplayMenu();
            string orderItem = GetCustomerOrder();

            Console.WriteLine($"How many {orderItem} pizzas would you like to order? (1-10)");
            int itemQuantity = GetPizzaQuantity();
            if (orderList.ContainsKey(orderItem))
            {
                orderList[orderItem] += itemQuantity;
            }
            else
            {
                orderList.Add(orderItem, itemQuantity);
            }

            Console.WriteLine("Current Order:".PadLeft(25, '-').PadRight(40, '-'));
            DisplayCurrentOrder(orderList);
            Console.WriteLine("".PadLeft(40, '-'));
        } while (WantsToAddToOrder());

        totalCost = GetOrderSubtotal(orderList);
        discount = GetOrderDiscount(orderList);
        deliveryFee = deliveryFees[customerPlanet];
        PrintReceipt(customerName, customerPlanet, totalCost, discount, deliveryFee, orderList);

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
    public static string GetCustomerOrder()
    {
        int itemNumber = 0;
        string[] validOrders = { "1", "2", "3", "4" };
        bool isValidOrder = false;

        do
        {
            string currentOrderItem = Console.ReadLine();

            if (int.TryParse(currentOrderItem, out itemNumber) && validOrders.Contains(currentOrderItem))
            {
                itemNumber = int.Parse(currentOrderItem);
                isValidOrder = true;
            }
            else
            {
                Console.WriteLine("Invalid input. Please select a menu item (1, 2, 3, or 4).");
            }
        } while (!isValidOrder);
        return menu.Keys.ElementAt(itemNumber - 1);
    }

    public static int GetPizzaQuantity()
    {
        string input = "";
        int itemAmount = 0;
        bool isValidInput = false;

        do
        {
            input = Console.ReadLine();

            if (!int.TryParse(input, out itemAmount) || !(itemAmount > 0) || !(itemAmount <= 10))
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 10.");
            }
            else
            {
                itemAmount = int.Parse(input);
                isValidInput = true;
            }
        } while (!isValidInput);
        return itemAmount;
    }

    public static void DisplayCurrentOrder(Dictionary<string, int> orderList)
    {
        foreach (KeyValuePair<string, int> item in orderList)
        {
            Console.WriteLine($"{item.Value} x {item.Key} @ {menu[item.Key]} = {item.Value * menu[item.Key]} credits");
        }
    }

    public static bool WantsToAddToOrder()
    {
        string response;
        Console.WriteLine("Would you like to add another pizza to your order? (yes/no)");
        do
        {
            response = Console.ReadLine().ToLower();
            if (response == "no")
            {
                return false;
            }
            else if (response == "yes")
            {
                return true;
            }
            Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
        } while (response != "yes" || response != "no");
        return false;
    }

    public static int GetOrderSubtotal(Dictionary<string, int> orderList)
    {
        int orderSubtotal = 0;
        foreach (KeyValuePair<string, int> item in orderList)
        {
            orderSubtotal += menu[item.Key] * item.Value;
        }

        return orderSubtotal;
    }

    public static double GetOrderDiscount(Dictionary<string, int> orderList)
    {
        double discount = 0;
        int pizzaCount = 0;

        foreach (KeyValuePair<string, int> item in orderList)
        {
            pizzaCount += item.Value;
        }

        if (pizzaCount >= 3)
        {
            discount = 0.1;
        }

        return discount;
    }

    public static void PrintReceipt(string customerName, string customerPlanet, int totalCost, double discount, int deliveryFee, Dictionary<string, int> orderList)
    {
        Random random = new Random();
        int deliveryTime = random.Next(20, 61);

        Console.WriteLine("".PadLeft(40, '-'));
        Console.WriteLine($"Order for {customerName} to {customerPlanet}");
        Console.WriteLine("".PadLeft(40, '-'));
        DisplayCurrentOrder(orderList);
        Console.WriteLine($"Subtotal: {totalCost} credits");
        Console.WriteLine($"Discount ({discount * 100}%): {-Math.Round(totalCost * discount, 2)} credits");
        Console.WriteLine($"Delivery Fee: {deliveryFee} credits");
        Console.WriteLine($"Total Due: {(totalCost - (totalCost * discount)) + deliveryFee} credits");
        Console.WriteLine("".PadLeft(40, '-'));
        Console.WriteLine("Thank you for your order! Your pizza is on the way!");
        Console.Write($"Estimeted delivery time: {deliveryTime} minutes");
    }
}
