namespace GoldenTicket.Models.TicketsViewModels
{
    /// <summary>
    /// Tech time model
    /// </summary>
    public class TechnicianTime
    {
        /// <summary>
        /// Get's technician
        /// </summary>
        /// <returns>technician</returns>
        public Technician Technician { get; set; }

        /// <summary>
        /// get's time
        /// </summary>
        /// <returns>technician's ticket time</returns>
        public TechnicianTicketTime Time { get; set; }
    }
}