namespace GoldenTicket.Models
{
    /// <summary>
    /// token request model
    /// </summary>
    public class TokenRequest
    {
        /// <summary>
        /// Gets user's username
        /// </summary>
        /// <returns>username string</returns>
        public string Username { get; set; }

        /// <summary>
        /// Gets user's password
        /// </summary>
        /// <returns>password string</returns>
        public string Password { get; set; }
    }
}