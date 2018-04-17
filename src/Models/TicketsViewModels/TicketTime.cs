using System;

namespace GoldenTicket.Models.TicketsViewModels
{
    public class TicketTime
    {
        public string TicketTitle { get; set; }

        public Guid TicketId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}