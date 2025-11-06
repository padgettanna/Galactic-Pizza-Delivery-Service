using System.Data.SqlTypes;

namespace Galactic_Pizza_Delivery_Service;

class Program
{
    static void Main(string[] args)
    {
        string[] validPlanets = { "Earth", "Mars", "jupiter Station", "Venus Outpost" };
        int totalCost = 0;
        int deliveryFee = 0;
        bool wantsToAddMore = true;

        Console.WriteLine("Welcome to the Galactic Pizze Delivery Service!");
        Console.WriteLine("Please enter your name:");
        string customerName = Console.ReadLine();
        Console.WriteLine($"Hello, {customerName}! Which planet are you on? ");
        string customerPlanet = Console.ReadLine();

        Console.WriteLine($"Great! We deliver to {customerPlanet}. Here are the meny options:");
        Console.WriteLine("       Pizza Type     |  Cost (credits)    \n  1. Galactic Cheese   |    10    \n  2. Meteor Meat Lover |    15    \n  3. Veggie  Nebula    |    12    \n  4. Black Hole BBQ    |    18");

        do
        {
            totalCost = GetCustomerOrder(totalCost);

            Console.WriteLine("Would you like to add another pizza to your order? (y/n)");
            string addMoreResponse = Console.ReadLine().ToLower();
            if (addMoreResponse != "y")
            {
                wantsToAddMore = false;
                deliveryFee = GetDeliveryFee(customerPlanet.ToLower());
                Console.WriteLine($"Order for {customerName} to {customerPlanet}");
                Console.WriteLine($"Subtotal: {totalCost} credits.");
                Console.WriteLine($"Delivery fee: {deliveryFee} credits");
                Console.WriteLine($"Total Due: {totalCost + deliveryFee} credits.");
            }

        } while (wantsToAddMore);

    }

    public static int GetCustomerOrder(int totalCost)
    {
        string orderItem = "";
        string[] validOrders = { "1", "2", "3", "4" };
        bool isValidOrder = false;

        Console.WriteLine("Please select the number of pizza from the menu:");

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

        switch (orderItem)
        {
            case "1":
                totalCost += 10;
                break;
            case "2":
                totalCost += 15;
                break;
            case "3":
                totalCost += 12;
                break;
            case "4":
                totalCost += 18;
                break;
            default:
                break;
        }
        Console.WriteLine($"Your current total is {totalCost} credits.");
        return totalCost;
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

}
