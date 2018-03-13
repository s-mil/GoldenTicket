using System;

namespace GoldenTicket.WebApi.Models
{
    /// <summary>
    /// The pivot table model for many tickets to many technicians
    /// </summary>
    public class TechnicianTicket
    {
        /// <summary>
        /// The technician id
        /// </summary>
        public Guid TechnicianId { get; set; }
        
        /// <summary>
        /// The ticket id
        /// </summary>
        public Guid TicketId { get; set; }
    }
}