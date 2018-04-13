using System;
using System.ComponentModel.DataAnnotations;

namespace GoldenTicket.Models
{
    /// <summary>
    /// The pivot table model for many tickets to many technicians and for holding time worked data
    /// </summary>
    public class TechnicianTicketTime
    {
        /// <summary>
        /// The id.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// The technician id
        /// </summary>
        public string TechnicianId { get; set; }

        /// <summary>
        /// The ticket id
        /// </summary>
        public Guid TicketId { get; set; }

        /// <summary>
        /// The start time
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// The end time
        /// </summary>
        public DateTime End { get; set; }
    }
}