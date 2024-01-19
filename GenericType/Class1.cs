using System;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int CategoryId { get; set; }

    public delegate void ItemUpdatedEventHandler(object sender, ItemUpdatedEventArgs e);
    public event ItemUpdatedEventHandler ItemUpdated;

    public void UpdateItem(double newPrice, string newName)
    {
        if (newPrice < 0)
        {
            throw new ArgumentException("Price cannot be negative.");
        }

        double oldPrice = Price;
        string oldName = Name;
        Price = newPrice;
        Name = newName;
        ItemUpdated?.Invoke(this, new ItemUpdatedEventArgs(oldPrice, newPrice, oldName, newName));
    }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class ItemUpdatedEventArgs : EventArgs
{
    public double OldPrice { get; }
    public double NewPrice { get; }
    public string OldName { get; }
    public string NewName { get; }

    public ItemUpdatedEventArgs(double oldPrice, double newPrice, string oldName, string newName)
    {
        OldPrice = oldPrice;
        NewPrice = newPrice;
        OldName = oldName;
        NewName = newName;
    }
}

public class ItemManager
{
    private string managerName;

    public ItemManager(string name)
    {
        managerName = name;
    }

    public void Subscribe(Item.ItemUpdatedEventHandler handler)
    {
        Item.ItemUpdated += handler;
        Console.WriteLine($"{managerName}: ItemUpdated event handler subscribed.");
    }

    public void Unsubscribe(Item.ItemUpdatedEventHandler handler)
    {
        Item.ItemUpdated -= handler;
        Console.WriteLine($"{managerName}: ItemUpdated event handler unsubscribed.");
    }
}

class Program
{
    static void Main()
    {
        ItemManager itemManager = new ItemManager("ItemManager1");
        Item item1 = new Item { Id = 1, Name = "Product A", Price = 20.0, CategoryId = 1 };
        Item item2 = new Item { Id = 2, Name = "Product B", Price = 30.0, CategoryId = 2 };

        itemManager.Subscribe(item1.ItemUpdated);
        itemManager.Subscribe(item2.ItemUpdated);

        try
        {
            item1.UpdateItem(25.0, "Updated Product A");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }

        itemManager.Unsubscribe(item1.ItemUpdated);
        item2.UpdateItem(35.0, "Updated Product B");
    }
}
