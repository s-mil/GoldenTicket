using System;

namespace GoldenTicket.WebApi.Models
{
    /// <summary>
    /// A ticket for a client
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// The Id for this ticket
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// The Id for the client who owns this ticket
        /// </summary>
        Guid ClientId { get; set; }

        /// <summary>
        /// The title of this ticket
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// The description for this ticket
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// The complexity (1-3) of this ticket
        /// </summary>
        int Complexity { get; set; }

        /// <summary>
        /// Defines if this ticket is urgent
        /// </summary>
        bool IsUrgent { get; set; }

        /// <summary>
        /// Notes for this ticket
        /// </summary>
        string Notes { get; set; }
    }
}