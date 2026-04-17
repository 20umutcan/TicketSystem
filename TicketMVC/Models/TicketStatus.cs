namespace TicketMVC.Models
{
    public enum TicketStatus
    {
        Open = 0,
        InProgress = 1,
        Resolved = 2,
        Closed = 3
    }

    public enum TicketPriority
    {
        Low = 0,
        Normal = 1,
        High = 2,
        Critical = 3
    }
}