using System;

namespace GoldenTicket.Models.TicketsViewModels
{
    /// <summary>
    /// Relates time and ticket information.
    /// </summary>
    public class TicketTime
    {
        /// <summary>
        /// The title of the ticket.
        /// </summary>
        public string TicketTitle { get; set; }

        /// <summary>
        /// The id of the ticket.
        /// </summary>
        /// <returns></returns>
        public Guid TicketId { get; set; }

        /// <summary>
        /// The start time.
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// The end time.
        /// </summary>
        public DateTime End { get; set; }
    }
}