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
        public DbSet<TechnicianTicket> TechnicianTickets { get; set; }

        /// <summary>
        /// The constructor for this context
        /// </summary>
        /// <param name="options">The options to create the context around</param>
        /// <returns>A new instance of this context</returns>
        public GoldenTicketContext(DbContextOptions<GoldenTicketContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Technicians
			modelBuilder.Entity<Technician>().HasKey(technician => technician.Id);
			modelBuilder.Entity<Technician>().Property(Technician => Technician.Id).ValueGeneratedOnAdd().IsRequired();

            //Clients
			modelBuilder.Entity<Client>().HasKey(client => client.Id);
			modelBuilder.Entity<Client>().Property(client => client.Id).ValueGeneratedOnAdd().IsRequired();

            //Tickets
			modelBuilder.Entity<Ticket>().HasKey(ticket => ticket.Id);
			modelBuilder.Entity<Ticket>().Property(ticket => ticket.Id).ValueGeneratedOnAdd().IsRequired();
            
            //TechnicianTickets
			modelBuilder.Entity<TechnicianTicket>().HasKey(technicianTicket => new { technicianTicket.TechnicianId, technicianTicket.TicketId });
			modelBuilder.Entity<TechnicianTicket>().Property(technicianTicket => technicianTicket.TechnicianId).IsRequired();
			modelBuilder.Entity<TechnicianTicket>().Property(technicianTicket => technicianTicket.TicketId).IsRequired();
        }
    }
}