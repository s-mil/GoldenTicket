using System.Collections.Generic;

namespace GoldenTicket.Models.TicketsViewModels
{
    public class TicketDetails
    {
        public Ticket Ticket { get; set; }

        public Client Client { get; set; }

        public IEnumerable<TechnicianTicketTime> Times { get; set; }
    }
}