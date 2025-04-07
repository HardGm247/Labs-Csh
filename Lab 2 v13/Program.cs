using System;
using System.Collections.Generic;
using System.Linq;

public class InventoryLog
{
    // Приватные поля
    private Dictionary<string, int> items;
    private List<string> transactions;
    private DateTime logDate;

    // Свойства
    public DateTime LogDate => logDate;
    public int ItemsCount => items.Count;
    public int TransactionsCount => transactions.Count;

    // Конструктор
    public InventoryLog()
    {
        items = new Dictionary<string, int>();
        transactions = new List<string>();
        logDate = DateTime.Now;
    }

    // Методы (AddItem, RemoveItem и т.д.)
    public void AddItem(string itemName, int quantity)
    {
        if (quantity < 0)
            throw new ArgumentException("Количество не может быть отрицательным!");

        if (items.ContainsKey(itemName))
            items[itemName] += quantity;
        else
            items.Add(itemName, quantity);

        AddTransaction($"Добавлено: {itemName} x{quantity}");
    }

    public bool RemoveItem(string itemName, int quantity)
    {
        if (quantity < 0 || !items.ContainsKey(itemName) || items[itemName] < quantity)
        {
            AddTransaction($"Ошибка списания: {itemName} x{quantity} (недостаточно)");
            return false;
        }

        items[itemName] -= quantity;
        if (items[itemName] == 0)
            items.Remove(itemName);

        AddTransaction($"Списано: {itemName} x{quantity}");
        return true;
    }

    public void AddTransaction(string details)
    {
        transactions.Add($"{DateTime.Now:HH:mm:ss}: {details}");
    }

    public string GetInventoryInfo()
    {
        return items.Count == 0
            ? "Склад пуст."
            : "Товары:\n" + string.Join("\n", items.Select(x => $"- {x.Key}: {x.Value} шт."));
    }

    public string GetTransactionsLog()
    {
        return transactions.Count == 0
            ? "Операций нет."
            : "Лог операций:\n" + string.Join("\n", transactions);
    }

    //  Статический метод Main (точка входа)
    public static void Main()
    {
        Console.WriteLine("=== Журнал учета склада ===");
        InventoryLog log = new InventoryLog();

        // Добавляем товар
        log.AddItem("Ноутбук", 5);
        log.AddItem("Монитор", 10);

        // Пытаемся списать
        log.RemoveItem("Монитор", 3);
        log.RemoveItem("Ноутбук", 10); // Неудачная попытка

        // Результаты 
        Console.WriteLine(log.GetInventoryInfo());
        Console.WriteLine("\n" + log.GetTransactionsLog());

        Console.ReadKey(); // Чтобы консоль не закрылась
    }
}