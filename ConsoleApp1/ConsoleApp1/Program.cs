using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaApp;

public abstract class MenuItem
{
    public string Title { get; }

    protected decimal BasePrice { get; }

    protected MenuItem(string title, decimal basePrice)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Item name cannot be empty.", nameof(title));

        if (basePrice < 0)
            throw new ArgumentOutOfRangeException(nameof(basePrice), "Price cannot be negative.");

        Title = title;
        BasePrice = basePrice;
    }

    public abstract decimal CalculateCost();

    public abstract string Describe();

    public virtual string GetTypeName() => "Menu item";
}

// Интерфейс для позиций, к которым можно применить скидку
public interface IDiscountable
{
    decimal CalculateDiscountedCost(decimal discountPercent);

    string GetDiscountDescription();
}

// Пицца
public class Pizza : MenuItem, IDiscountable
{
    private const decimal ToppingPrice = 45m;
    private const decimal ManyToppingsCoefficient = 0.94m;
    private const int ManyToppingsLimit = 4;

    public int ToppingCount { get; }

    public Pizza(string title, decimal basePrice, int toppingCount)
        : base(title, basePrice)
    {
        if (toppingCount < 0)
            throw new ArgumentOutOfRangeException(nameof(toppingCount),
                "Topping count cannot be negative.");

        ToppingCount = toppingCount;
    }

    public override decimal CalculateCost()
    {
        return BasePrice + ToppingCount * ToppingPrice;
    }

    public override string Describe()
    {
        return $"Pizza \"{Title}\", toppings: {ToppingCount}, total: {CalculateCost():F2} RUB.";
    }

    public override string GetTypeName() => "Pizza";

    public decimal CalculateDiscountedCost(decimal discountPercent)
    {
        CheckDiscount(discountPercent);

        decimal price = CalculateCost() * (1m - discountPercent / 100m);

        if (ToppingCount >= ManyToppingsLimit)
            price *= ManyToppingsCoefficient;

        return Math.Round(price, 2);
    }

    public string GetDiscountDescription()
    {
        return ToppingCount >= ManyToppingsLimit
            ? "Additional 6% discount for many toppings is applied."
            : "Additional topping discount is not applied.";
    }

    private static void CheckDiscount(decimal percent)
    {
        if (percent < 0 || percent > 100)
            throw new ArgumentOutOfRangeException(nameof(percent),
                "Discount must be between 0 and 100 percent.");
    }
}

// Напиток
public class Drink : MenuItem
{
    private const decimal PricePerHundredMl = 0.4m;

    public int VolumeMl { get; }

    public Drink(string title, decimal basePrice, int volumeMl)
        : base(title, basePrice)
    {
        if (volumeMl <= 0)
            throw new ArgumentOutOfRangeException(nameof(volumeMl),
                "Drink volume must be greater than zero.");

        VolumeMl = volumeMl;
    }

    public override decimal CalculateCost()
    {
        return BasePrice + VolumeMl / 100m * PricePerHundredMl;
    }

    public override string Describe()
    {
        return $"Drink \"{Title}\", volume: {VolumeMl} ml, total: {CalculateCost():F2} RUB.";
    }

    public override string GetTypeName() => "Drink";
}

// Десерт
public class Dessert : MenuItem, IDiscountable
{
    private const decimal NotFreshCoefficient = 0.75m;
    private const decimal NotFreshExtraDiscount = 0.91m;

    public bool MadeToday { get; }

    public Dessert(string title, decimal basePrice, bool madeToday)
        : base(title, basePrice)
    {
        MadeToday = madeToday;
    }

    public override decimal CalculateCost()
    {
        return MadeToday ? BasePrice : BasePrice * NotFreshCoefficient;
    }

    public override string Describe()
    {
        string state = MadeToday ? "made today" : "not made today";

        return $"Dessert \"{Title}\", {state}, total: {CalculateCost():F2} RUB.";
    }

    public override string GetTypeName() => "Dessert";

    public decimal CalculateDiscountedCost(decimal discountPercent)
    {
        if (discountPercent < 0 || discountPercent > 100)
            throw new ArgumentOutOfRangeException(nameof(discountPercent),
                "Discount must be between 0 and 100 percent.");

        decimal price = CalculateCost() * (1m - discountPercent / 100m);

        if (!MadeToday)
            price *= NotFreshExtraDiscount;

        return Math.Round(price, 2);
    }

    public string GetDiscountDescription()
    {
        return MadeToday
            ? "Additional discount is not applied."
            : "Additional 9% discount is applied.";
    }
}

// Комбо из нескольких позиций
public class Combo : MenuItem
{
    private const decimal ComboCoefficient = 0.82m;

    private readonly List<MenuItem> _items;

    public IReadOnlyList<MenuItem> Items => _items;

    public Combo(string title, IEnumerable<MenuItem> items)
        : base(title, 0m)
    {
        if (items == null)
            throw new ArgumentNullException(nameof(items));

        _items = items.ToList();

        if (_items.Count == 0 || _items.Any(item => item == null))
            throw new ArgumentException("A combo must contain at least one menu item.");
    }

    public override decimal CalculateCost()
    {
        decimal total = _items.Sum(item => item.CalculateCost());
        return Math.Round(total * ComboCoefficient, 2);
    }

    public override string Describe()
    {
        return $"Combo \"{Title}\", items: {_items.Count}, discount: 18%, total: {CalculateCost():F2} RUB.";
    }

    public override string GetTypeName() => "Combo";
}

// Заказ
public class Order
{
    private const int MaxItems = 20;

    private readonly List<MenuItem> _items = new();

    public int Count => _items.Count;

    public bool IsFull => _items.Count >= MaxItems;

    public IReadOnlyList<MenuItem> Items => _items;

    public void AddItem(MenuItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        if (IsFull)
            throw new InvalidOperationException(
                $"The order already contains the maximum number of items: {MaxItems}.");

        _items.Add(item);
    }

    public decimal GetTotal()
    {
        return _items.Sum(item => item.CalculateCost());
    }

    public decimal GetTotalWithDiscount(decimal discountPercent)
    {
        if (discountPercent < 0 || discountPercent > 100)
            throw new ArgumentOutOfRangeException(nameof(discountPercent),
                "Discount must be between 0 and 100 percent.");

        decimal total = 0m;

        foreach (MenuItem item in _items)
        {
            if (item is IDiscountable discountable)
                total += discountable.CalculateDiscountedCost(discountPercent);
            else
                total += item.CalculateCost();
        }

        return Math.Round(total, 2);
    }

    public MenuItem? GetMostExpensiveItem()
    {
        return _items
            .OrderByDescending(item => item.CalculateCost())
            .FirstOrDefault();
    }

    public IReadOnlyList<MenuItem> GetItemsSortedByPrice()
    {
        return _items
            .OrderBy(item => item.CalculateCost())
            .ToList();
    }

    public void PrintReceipt()
    {
        Console.WriteLine();
        Console.WriteLine("--------------- RECEIPT ---------------");

        if (_items.Count == 0)
        {
            Console.WriteLine("The order is empty.");
        }
        else
        {
            foreach (MenuItem item in _items)
            {
                Console.WriteLine($"{item.GetTypeName()}: {item.Describe()}");
            }
        }

        Console.WriteLine("---------------------------------------");
        Console.WriteLine($"Total: {GetTotal():F2} RUB.");
        Console.WriteLine("---------------------------------------");
    }
}

// Точка входа программы
internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        List<MenuItem> menu = new()
        {
            new Pizza("Margherita", 390m, 2),
            new Pizza("Pepperoni", 320m, 3),
            new Pizza("Four Cheeses", 460m, 5),

            new Drink("Cola", 90m, 330),
            new Drink("Orange Juice", 110m, 250),

            new Dessert("Tiramisu", 310m, true),
            new Dessert("Cheesecake", 270m, false),

            new Combo("Lunch Set", new MenuItem[]
            {
                new Drink("Cola", 90m, 330),
                new Dessert("Cheesecake", 270m, false)
            })
        };

        Order order = new();

        Console.WriteLine("*************************");
        Console.WriteLine("      PIZZERIA FORZA     ");
        Console.WriteLine("*************************");

        bool addMore = true;

        while (addMore && !order.IsFull)
        {
            ShowMenu(menu);

            int choice = ReadMenuNumber(menu.Count);
            MenuItem selectedItem = menu[choice - 1];

            try
            {
                order.AddItem(selectedItem);
                Console.WriteLine($"Added: {selectedItem.Title}.");
                Console.WriteLine($"Items in order: {order.Count}/20.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Could not add the item: {ex.Message}");
                break;
            }

            addMore = ReadYesNo("\nAdd another item? (y/n): ");
        }

        order.PrintReceipt();

        if (order.Count > 0 && ReadYesNo("\nApply a 10% discount? (y/n): "))
        {
            decimal totalWithoutDiscount = order.GetTotal();
            decimal totalWithDiscount = order.GetTotalWithDiscount(10m);

            Console.WriteLine();
            Console.WriteLine("========== DISCOUNT ==========");
            Console.WriteLine($"Total without discount: {totalWithoutDiscount:F2} RUB.");
            Console.WriteLine($"Total with discount: {totalWithDiscount:F2} RUB.");
            Console.WriteLine("==============================");
        }

        MenuItem? mostExpensive = order.GetMostExpensiveItem();

        if (mostExpensive != null)
        {
            Console.WriteLine(
                $"\nThe most expensive item: {mostExpensive.Title} — {mostExpensive.CalculateCost():F2} RUB.");
        }

        Console.WriteLine("\nItems sorted by price, from lowest to highest:");

        foreach (MenuItem item in order.GetItemsSortedByPrice())
        {
            Console.WriteLine($"{item.Title} — {item.CalculateCost():F2} RUB.");
        }

        Console.WriteLine("\nThank you for your order!");
    }

    private static void ShowMenu(List<MenuItem> menu)
    {
        Console.WriteLine();
        Console.WriteLine("=========== MENU ===========");

        for (int i = 0; i < menu.Count; i++)
        {
            MenuItem item = menu[i];

            Console.WriteLine(
                $"{i + 1}. {item.GetTypeName()} — {item.Title} " +
                $"({item.CalculateCost():F2} RUB.)");
        }

        Console.WriteLine("============================");
    }

    private static int ReadMenuNumber(int maxNumber)
    {
        while (true)
        {
            Console.Write($"Enter a number from 1 to {maxNumber}: ");

            string? input = Console.ReadLine();

            if (int.TryParse(input, out int number) && number >= 1 && number <= maxNumber)
            {
                return number;
            }

            Console.WriteLine("Invalid number. Please try again.");
        }
    }

    private static bool ReadYesNo(string message)
    {
        while (true)
        {
            Console.Write(message);

            string? answer = Console.ReadLine()?.Trim().ToLowerInvariant();

            if (answer == "y" || answer == "yes")
                return true;

            if (answer == "n" || answer == "no")
                return false;

            Console.WriteLine("Please enter 'y' (yes) or 'n' (no).");
        }
    }
}