using Microsoft.EntityFrameworkCore;

namespace GoldenTicket.WebApi.Models
{
    /// <summary>
    /// The context for the GoldenTicket Api
    /// </summary>
    public class GoldenTicketContext : DbContext
    {
        /// <summary>
        /// The collection of technicians
        /// </summary>
        public DbSet<Technician> Technicians { get; set; }

        /// <summary>
        /// The collection of clients
        /// </summary>
        public DbSet<Client> Clients { get; set; }

        /// <summary>
        /// The collection of tickets
        /// </summary>
        public DbSet<Ticket> Tickets { get; set; }

        /// <summary>
        /// The collection of TechnicianTicket pivot models
        /// </summary>
        public DbSet<TechnicianTicketTime> TechnicianTicketTimes { get; set; }

        /// <summary>
        /// The constructor for this context
        /// </summary>
        /// <param name="options">The options to create the context around</param>
        /// <returns>A new instance of this context</returns>
        public GoldenTicketContext(DbContextOptions<GoldenTicketContext> options) : base(options)
        { }
    }
}