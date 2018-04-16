using System.Collections.Generic;
using GoldenTicket.Models;

namespace GoldenTicket.Models.ClientsViewModels
{
    public class ClientDetails
    {
        public Client Client { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}