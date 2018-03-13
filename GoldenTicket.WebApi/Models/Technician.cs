using System;

namespace GoldenTicket.WebApi.Models
{
    /// <summary>
    /// A Technician and information
    /// </summary>
    public class Technician
    {
        /// <summary>
        /// The Id for the Technician
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Is the Technician an Admin?
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// The name of the Technician
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The Date the technician object was created
        /// </summary>
        public DateTime DateAdded { get; set; }
    }
}
