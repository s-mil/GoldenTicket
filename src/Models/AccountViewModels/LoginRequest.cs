using System.ComponentModel.DataAnnotations;

namespace GoldenTicket.Models.AccountViewModels
{
    /// <summary>
    /// Login information request model
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Gets user's username
        /// </summary>
        /// <returns>username string</returns>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Gets user's password
        /// </summary>
        /// <returns>password string</returns>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Option to remember user/password
        /// </summary>
        /// <returns>bool</returns>
        public bool RememberMe { get; set; }
    }
}