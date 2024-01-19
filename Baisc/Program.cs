
namespace basic
{
    class Item
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public Item(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }

    class Menu
    {
        public Item[] Items { get; set; }

        public Menu()
        {

            Items =
            [
            new Item("Tiffin", 100),
                new Item("Lunch", 200),
                new Item("others", 300),

            ];
        }
    }

    class Customer
    {
        public required string Name { get; set; }
        public int Age { get; set; }
        public double amount { get; set; }

        public void PlaceOrder(string selectedItem, int quantity, Menu menu)
        {

            Item selectedFoodItem = Array.Find(menu.Items, item => item.Name.Equals(selectedItem, StringComparison.OrdinalIgnoreCase));

            if (selectedFoodItem != null)
            {
                double totalOrderCost = quantity * selectedFoodItem.Price;
                if (amount >= totalOrderCost)
                {
                    amount -= totalOrderCost;
                    Console.WriteLine($"Order placed by {Name} for {quantity} {selectedItem}(s).");
                    Console.WriteLine($"Total Order Cost: ${totalOrderCost:F2}");                   
                }
                else
                {
                    Console.WriteLine(" you don't have enough money");
                }
            }
            else
            {
                Console.WriteLine("this item is not available in the menu.");
            }
        }
    }

    class FoodDeliveryApp
    {
        static void Main()
        {
            Customer customer = new()
            {
                Name = "ABCD",
                Age = 28,
                amount = 200
            };

            Menu menu = new();
            customer.PlaceOrder("Tiffin", 2, menu);
            Console.WriteLine("\nMenu:");
            foreach (var item in menu.Items)
            {
                Console.WriteLine($"{item.Name} - ${item.Price:F2}");
            }
        }
    }
}

