using LineCon.Models;
using System;

namespace LineCon.Data.Exceptions
{
    public class TicketWindowFullException : Exception
    {
        public TicketWindow TicketWindow { get; set; }

        public TicketWindowFullException(TicketWindow ticketWindow)
            :base($"The ticket window at {ticketWindow.StartTime} is full")
        {
            TicketWindow = ticketWindow;
        }
    }
}
