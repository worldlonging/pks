using System;
using System.Collections.Generic;
using System.Linq;

public class Order
{
    private static Dictionary<int, int> closedOrdersByWaiter = new Dictionary<int, int>();

    public int Id { get; set; }
    public int TableId { get; set; }
    public List<Dish> Dishes { get; set; }
    public string Comment { get; set; }
    public DateTime OrderTime { get; set; }
    public int WaiterId { get; set; }
    public DateTime? CloseTime { get; private set; }
    public double TotalCost => Dishes.Sum(d => d.Price);

    public Order(int id, int tableId, int waiterId, List<Dish> dishes, string comment, DateTime orderTime)
    {
        Id = id;
        TableId = tableId;
        WaiterId = waiterId;
        Dishes = dishes;
        Comment = comment;
        OrderTime = orderTime;
        CloseTime = null;
    }

    public void EditOrder(int? tableId = null, int? waiterId = null, List<Dish> dishes = null, string comment = null)
    {
        if (tableId != null) TableId = (int)tableId;
        if (waiterId != null) WaiterId = (int)waiterId;
        if (dishes != null) Dishes = dishes;
        if (comment != null) Comment = comment;
    }

    public void DisplayOrder()
    {
        Console.WriteLine($"Заказ ID: {Id}, Столик ID: {TableId}, Официант ID: {WaiterId}, Время заказа: {OrderTime}, Комментарий: {Comment}");
        Console.WriteLine("Блюда:");
        var groupedDishes = Dishes.GroupBy(d => d.Id);
        foreach (var group in groupedDishes)
        {
            var dish = group.First();
            Console.WriteLine($"- {dish.Name} x{group.Count()} ({group.Sum(g => g.Price):F2} руб.)");
        }

        if (CloseTime == null)
        {
            Console.WriteLine("\nЗаказ еще не закрыт.");
        }
    }

    public void CloseOrder(DateTime closeTime)
    {
        CloseTime = closeTime;
        if (closedOrdersByWaiter.ContainsKey(WaiterId))
        {
            closedOrdersByWaiter[WaiterId]++;
        }
        else
        {
            closedOrdersByWaiter[WaiterId] = 1;
        }
    }

    public static int GetClosedOrderCountByWaiter(int waiterId)
    {
        return closedOrdersByWaiter.ContainsKey(waiterId) ? closedOrdersByWaiter[waiterId] : 0;
    }

    public void PrintReceipt()
    {
        if (CloseTime == null)
        {
            Console.WriteLine("Заказ еще не закрыт.");
            return;
        }

        Console.WriteLine("----------------------------------------------------------------");
        var groupedDishes = Dishes.GroupBy(d => d.Category);
        double finalTotal = 0;

        foreach (var group in groupedDishes)
        {
            Console.WriteLine($"\nКатегория: {group.Key}");
            double subTotal = 0;

            foreach (var dishGroup in group.GroupBy(d => d.Id))
            {
                var dish = dishGroup.First();
                var count = dishGroup.Count();
                double dishTotal = count * dish.Price;
                subTotal += dishTotal;
                Console.WriteLine($"{dish.Name} - {count} x {dish.Price:F2} руб. = {dishTotal:F2} руб.");
            }
            Console.WriteLine($"Итог по категории: {subTotal:F2} руб.");
            finalTotal += subTotal;
        }
        Console.WriteLine("----------------------------------------------------------------");
        Console.WriteLine($"Период обслуживания {OrderTime} - {CloseTime}");
        Console.WriteLine($"Общая итоговая сумма: {finalTotal:F2} руб.");
        Console.WriteLine($"Официант с ID {WaiterId} закрыл {GetClosedOrderCountByWaiter(WaiterId)} заказ(ов).");

        Console.WriteLine("\nСтатистика заказанных блюд:");
        var dishStatistics = Dishes.GroupBy(d => d.Name).Select(g => new { Name = g.Key, Count = g.Count() });
        foreach (var stat in dishStatistics)
        {
            Console.WriteLine($"{stat.Name}: {stat.Count} раз(а)");
        }
    }
}
