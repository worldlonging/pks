using System.Text;
using System;
using System.Collections.Generic;

public class BookingSystem
{
    private List<Table> tables;
    private List<Reservation> reservations;

    public BookingSystem()
    {
        tables = new List<Table>();
        reservations = new List<Reservation>();
    }

    // Метод для добавления нового стола
    public void AddTable(int tableId, string location, int seats)
    {
        var table = new Table(tableId, location, seats);
        tables.Add(table);
        Console.WriteLine($"Стол с ID {tableId} добавлен.");
    }

    // Метод для создания новой брони
    public void AddReservation(int clientId, string clientName, string phone, int startTime, int endTime, string comment, int tableId)
    {
        var table = GetTableById(tableId);
        if (table == null)
        {
            Console.WriteLine($"Стол с ID {tableId} не найден.");
            return;
        }

        var reservation = new Reservation(clientId, clientName, phone, startTime, endTime, comment);
        if (reservation.CreateReservation(table))
        {
            reservations.Add(reservation);
            Console.WriteLine($"Бронь успешно создана для клиента {clientName} на стол {tableId}.");
        }
        else
        {
            Console.WriteLine($"Стол {tableId} не доступен в это время.");
        }
    }

    // Метод поиска стола по ID
    private Table GetTableById(int tableId)
    {
        return tables.Find(t => t.TableId == tableId);
    }

    // Метод для отображения информации о столе
    public void ShowTableInfo(int tableId)
    {
        var table = GetTableById(tableId);
        if (table != null)
        {
            Console.WriteLine(table.GetTableInfo());
        }
        else
        {
            Console.WriteLine($"Стол с ID {tableId} не найден.");
        }
    }

    // Метод поиска брони по имени клиента и последним четырем цифрам телефона
    public void FindReservation(string clientName, string phoneLastDigits)
    {
        foreach (var reservation in reservations)
        {
            if (reservation.ClientName == clientName && reservation.Phone.EndsWith(phoneLastDigits))
            {
                Console.WriteLine($"Найдено бронирование для {clientName}: Стол {reservation.ReservedTable.TableId}, Время {reservation.StartTime}:00-{reservation.EndTime}:00");
                return;
            }
        }
        Console.WriteLine("Бронирование не найдено.");
    }

    // Метод для вывода списка доступных столов по фильтру (время и минимальное количество мест)
    public void ListAvailableTables(int startTime, int endTime, int minSeats)
    {
        Console.WriteLine($"Доступные столы с {startTime}:00 до {endTime}:00 для {minSeats} мест:");
        foreach (var table in tables)
        {
            if (table.IsAvailable(startTime, endTime) && table.Seats >= minSeats)
            {
                Console.WriteLine($"Стол ID {table.TableId}, Мест: {table.Seats}, Расположение: {table.Location}");
            }
        }
    }

    // Метод для отображения всех бронирований
    public void ShowAllReservations()
    {
        if (reservations.Count == 0)
        {
            Console.WriteLine("Бронирований пока нет.");
        }
        else
        {
            Console.WriteLine("Список всех бронирований:");
            foreach (var reservation in reservations)
            {
                Console.WriteLine($"ID Клиента: {reservation.ClientId}, Имя: {reservation.ClientName}, Телефон: {reservation.Phone}, " +
                                  $"Стол: {reservation.ReservedTable.TableId}, Время: {reservation.StartTime}:00-{reservation.EndTime}:00");
            }
        }
    }

    // Метод для отмены бронирования
    public void CancelReservation(int clientId)
    {
        var reservation = reservations.Find(r => r.ClientId == clientId);
        if (reservation != null)
        {
            reservation.CancelReservation();
            reservations.Remove(reservation);
            Console.WriteLine($"Бронь клиента с ID {clientId} успешно отменена.");
        }
        else
        {
            Console.WriteLine($"Бронирование для клиента с ID {clientId} не найдено.");
        }
    }

    // Метод для обновления информации о столе
    public void UpdateTableInfo(int tableId, string newLocation = null, int? newSeats = null)
    {
        var table = GetTableById(tableId);
        if (table == null)
        {
            Console.WriteLine($"Стол с ID {tableId} не найден.");
            return;
        }

        // Вызываем UpdateTable и проверяем результат
        bool isUpdated = table.UpdateTable(newLocation, newSeats);
        if (isUpdated)
        {
            Console.WriteLine("Данные стола успешно обновлены.");
        }
        else
        {
            Console.WriteLine("Изменение данных стола не выполнено из-за активных бронирований.");
        }
    }

    // Запуск тестового интерфейса
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        var system = new BookingSystem();

        // Добавляем столы
        system.AddTable(1, "у окна", 4);
        system.AddTable(2, "у выхода", 6);

        // Создаем брони
        system.AddReservation(3, "Макс", "88005553535", 12, 15, "Бизнес встреча", 1);
        system.AddReservation(7, "Анна", "5745552377", 16, 17, "Романтический ужин", 1);

        system.UpdateTableInfo(1, "У выхода", 2);

        // Отображаем информацию о столе
        system.ShowTableInfo(1);

        // Проверяем доступные столы
        system.ListAvailableTables(10, 12, 4);

        // Ищем бронирование по имени и номеру телефона
        system.FindReservation("Анна", "2377");

        // Отображаем все бронирования
        system.ShowAllReservations();

        // Отменяем бронирование
        system.CancelReservation(3);
    }
}
