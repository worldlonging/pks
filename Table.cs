

public class Table
{
    // Приватные поля
    private int tableId;
    private string location;
    private int seats;
    private Dictionary<int, Reservation> schedule;

    // Свойства для доступа к полям
    public int TableId
    {
        get => tableId;
        private set => tableId = value;
    }

    public string Location
    {
        get => location;
        set => location = value;
    }

    public int Seats
    {
        get => seats;
        set => seats = value;
    }

    // Конструктор
    public Table(int tableId, string location, int seats)
    {
        this.tableId = tableId;
        this.location = location;
        this.seats = seats;
        CreateTable();
    }

    // Создание расписания
    private void CreateTable()
    {
        schedule = new Dictionary<int, Reservation>();
        for (int hour = 9; hour < 18; hour++)
        {
            schedule[hour] = null;
        }
    }

    public bool UpdateTable(string newLocation = null, int? newSeats = null)
    {
        foreach (var entry in schedule.Values)
        {
            if (entry != null)
            {
                return false;
            }
        }

        if (newLocation != null)
        {
            location = newLocation;
        }
        if (newSeats.HasValue)
        {
            seats = newSeats.Value;
        }
        return true;
    }


    public string GetTableInfo()
    {
        string info = $"ID: {tableId}\nРасположение: {location}\nКоличество мест: {seats}\nРасписание:\n";
        foreach (var entry in schedule)
        {
            int hour = entry.Key;
            Reservation reservation = entry.Value;
            if (reservation != null)
            {
                info += $"{hour}:00-{hour + 1}:00 --- ID {reservation.ClientId}, {reservation.ClientName}, {reservation.Phone}\n";
            }
            else
            {
                info += $"{hour}:00-{hour + 1}:00 --- Свободно\n";
            }
        }
        return info;
    }

    public bool IsAvailable(int startHour, int endHour)
    {
        for (int hour = startHour; hour < endHour; hour++)
        {
            if (schedule[hour] != null)
            {
                return false;
            }
        }
        return true;
    }

    public bool ReserveTable(Reservation reservation, int startHour, int endHour)
    {
        if (IsAvailable(startHour, endHour))
        {
            for (int hour = startHour; hour < endHour; hour++)
            {
                schedule[hour] = reservation;
            }
            return true;
        }
        return false;
    }

    public void CancelReservation(int startHour, int endHour)
    {
        for (int hour = startHour; hour < endHour; hour++)
        {
            schedule[hour] = null;
        }
    }
}
