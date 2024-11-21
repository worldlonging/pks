
public class Program
{
    public static void Main()
    {
        // Создание меню
        List<Dish> menu = new List<Dish>
        {
            //Напитки
            new Dish(1, "Кока кола", "Сладкая вода, фирменный рецепт", "400мл", 140.00, DishCategory.Напитки, 5),
            new Dish(2, "Пепси", "Сладкая вода", "250мл", 120, DishCategory.Напитки, 5),
            //Салаты
            new Dish(3, "Салат Цезарь", "Салат, Курочка, Сыр", "350г", 250.00, DishCategory.Салаты, 15, "Вегетарианское"),
            new Dish(4, "Салат Цезарь с креветками", "Салат, креветки, Сыр", "280г", 320.00, DishCategory.Салаты, 25, "Мясное"),
            //Супы
            new Dish(5, "Куриный суп", "Курица, Овощи", "300г", 180.00, DishCategory.Супы, 20, "Халяль"),
            new Dish(6, "Грибной суп", "Грибы, Овощи", "350г", 150.00, DishCategory.Супы, 25, "Овощное")
        };

        Console.Write("Введите ID столика: ");
        int tableId = int.Parse(Console.ReadLine());

        Console.Write("Введите ID официанта: ");
        int waiterId = int.Parse(Console.ReadLine());

        // Создание пустого заказа
        List<Dish> orderDishes = new List<Dish>();

        bool ordering = true;
        while (ordering)
        {
            // Вывод меню и выбор блюд
            DisplayMenu(menu);
            Console.Write("Введите ID блюда для добавления в заказ (или '0' чтобы завершить заказ): ");
            int dishId = int.Parse(Console.ReadLine());
            if (dishId == 0)
            {
                ordering = false;
            }
            else
            {
                Dish selectedDish = menu.FirstOrDefault(d => d.Id == dishId);
                if (selectedDish != null)
                {
                    orderDishes.Add(selectedDish);
                    Console.WriteLine($"{selectedDish.Name} добавлен в заказ.");
                }
                else
                {
                    Console.WriteLine("Блюдо с указанным ID не найдено.");
                }
            }
        }

        Console.Write("Введите комментарий к заказу (или оставьте пустым): ");
        string comment = Console.ReadLine();
        Console.WriteLine();

        // Создание заказа
        Order order = new Order(1, tableId, waiterId, orderDishes, comment, DateTime.Now);
        order.DisplayOrder();
        order.CloseOrder(DateTime.Now.AddMinutes(30));

        // Вывод заказа после его закрытия
        Console.WriteLine("----------------------------------------------------------------");
        Console.WriteLine("После закрытия заказа:\n");
        order.DisplayOrder();
        order.PrintReceipt();
    }


    public static void DisplayMenu(List<Dish> dishes)
    {
        Console.WriteLine("\nМеню:");
        var groupedDishes = dishes.GroupBy(d => d.Category);
        foreach (var group in groupedDishes)
        {
            Console.WriteLine($"\nКатегория: {group.Key}");
            foreach (var dish in group)
            {
                dish.DisplayDish();
            }
        }
        Console.WriteLine("----------------------------------------------------------------");
    }
}
