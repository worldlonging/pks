using System;

public class Dish
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Ingredients { get; set; }
    public string Weight { get; set; }
    public double Price { get; set; }
    public DishCategory Category { get; set; }
    public int CookingTime { get; set; }
    public string[] Types { get; set; }

    public Dish(int id, string name, string ingredients, string weight, double price, DishCategory category, int cookingTime, params string[] types)
    {
        Id = id;
        Name = name;
        Ingredients = ingredients;
        Weight = weight;
        Price = price;
        Category = category;
        CookingTime = cookingTime;
        Types = types;
    }

    public void EditDish(string name = null, string ingredients = null, string weight = null, double? price = null, DishCategory? category = null, int? cookingTime = null, params string[] types)
    {
        if (name != null) Name = name;
        if (ingredients != null) Ingredients = ingredients;
        if (weight != null) Weight = weight;
        if (price != null) Price = (double)price;
        if (category != null) Category = (DishCategory)category;
        if (cookingTime != null) CookingTime = (int)cookingTime;
        if (types.Length > 0) Types = types;
    }

    public void DisplayDish()
    {
        Console.WriteLine($"ID: {Id}, Название: {Name}, Состав: {Ingredients}, Вес: {Weight}, Цена: {Price:F2} руб, Время готовки: {CookingTime} мин, Тип: {string.Join(", ", Types)}");
    }

    public static void DeleteDish(ref Dish dish)
    {
        dish = null;
    }
}
