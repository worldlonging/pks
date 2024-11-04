public class Reservation
{
    // Приватные поля
    private int clientId;
    private string clientName;
    private string phone;
    private int startTime;
    private int endTime;
    private string comment;
    private Table reservedTable;

    // Свойства для доступа к полям
    public int ClientId
    {
        get => clientId;
        private set => clientId = value;
    }

    public string ClientName
    {
        get => clientName;
        set => clientName = value;
    }

    public string Phone
    {
        get => phone;
        set => phone = value;
    }

    public int StartTime
    {
        get => startTime;
        set => startTime = value;
    }

    public int EndTime
    {
        get => endTime;
        set => endTime = value;
    }

    public string Comment
    {
        get => comment;
        set => comment = value;
    }

    public Table ReservedTable
    {
        get => reservedTable;
        private set => reservedTable = value;
    }

    // Конструктор
    public Reservation(int clientId, string clientName, string phone, int startTime, int endTime, string comment)
    {
        this.clientId = clientId;
        this.clientName = clientName;
        this.phone = phone;
        this.startTime = startTime;
        this.endTime = endTime;
        this.comment = comment;
    }

    public bool CreateReservation(Table table)
    {
        if (table.ReserveTable(this, startTime, endTime))
        {
            reservedTable = table;
            return true;
        }
        return false;
    }

    public void UpdateReservation(int? newStartTime = null, int? newEndTime = null, Table newTable = null)
    {
        if (newStartTime.HasValue)
        {
            startTime = newStartTime.Value;
        }
        if (newEndTime.HasValue)
        {
            endTime = newEndTime.Value;
        }
        if (newTable != null && reservedTable != null)
        {
            reservedTable.CancelReservation(startTime, endTime);
            newTable.ReserveTable(this, startTime, endTime);
            reservedTable = newTable;
        }
    }

    public void CancelReservation()
    {
        if (reservedTable != null)
        {
            reservedTable.CancelReservation(startTime, endTime);
            reservedTable = null;
        }
    }
}
