using System;
using System.Collections.Generic;

public class Item
{
    public string Name { get; set; }
    public double Price { get; set; }
    public Item(string name, double price)
    {        Name = name;
        Price = price;
    }   
    public void DisplayInfo()
    {
        Console.WriteLine($"Item: {Name}, Price: ${Price}");
    }
}

public class Category
{
   
    public string Name { get; set; }
    private List<Item> items;
    public Category(string name)
    {
        Name = name;
        items = new List<Item>();
    }
    public void AddItem(Item item)
    {
        items.Add(item);
        Console.WriteLine($"{item.Name} added to {Name} category.");
    }
    public void DisplayItems()
    {
        Console.WriteLine($"Items in {Name} category:");
        foreach (var item in items)
        {
            item.DisplayInfo();
        }
    }
}

public class Store<T> where T : class
{ 
    private List<T> items;
    public Store()
    {
        items = new List<T>();
    }

    public void AddItem(T item)
    {
        items.Add(item);
        Console.WriteLine($"{item} added to the store.");
    }

    public void DisplayItems()
    {
        Console.WriteLine("Items in the store:");
        foreach (var item in items)
        {
            if (item is Item displayableItem)
            {
                displayableItem.DisplayInfo();
            }
            else
            {
               
                Console.WriteLine(item);
            }
        }
    }
}

public delegate void ItemAddedEventHandler(string itemName);
public class EventPublisher
{
    public event ItemAddedEventHandler ItemAdded;
    public void AddItemAndRaiseEvent(string itemName)
    {
        Console.WriteLine($"{itemName} added to the store.");  
        ItemAdded?.Invoke(itemName);
    }
}

class Program
{
    static void Main()
    {
        Item laptop = new Item("Laptop", 999.99);
        Item smartphone = new Item("Smartphone", 499.99);
        Category electronicsCategory = new Category("Electronics");
        Category clothingCategory = new Category("Clothing");
        electronicsCategory.AddItem(laptop);
        electronicsCategory.AddItem(smartphone);
        electronicsCategory.DisplayItems();
        Store<Item> generalStore = new Store<Item>();
        generalStore.AddItem(laptop);
        generalStore.AddItem(smartphone);
        generalStore.DisplayItems();        
        EventPublisher eventPublisher = new EventPublisher();      
        eventPublisher.ItemAdded += (itemName) => Console.WriteLine($"Event Handler: {itemName} added to the store.");
        eventPublisher.AddItemAndRaiseEvent("Headphones");
    }
}
