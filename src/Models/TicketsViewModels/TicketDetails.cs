using System.Collections.Generic;

namespace GoldenTicket.Models.TicketsViewModels
{
    public class TicketDetails
    {
        public Ticket Ticket { get; set; }

        public Client Client { get; set; }

        public List<TechnicianTime> Times { get; set; }
    }
}