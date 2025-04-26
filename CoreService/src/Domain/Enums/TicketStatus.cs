namespace CoreService.Domain.Enums
{
    public enum TicketStatus
    {
        Available = 0,   // nobody’s touched it yet
        Reserved = 1,    // held for someone, but not paid
        Sold = 2,        // paid for and issued
        Cancelled = 3,   // purchaser gave up or event cancelled
        Refunded = 4     // money was returned
    }
}

