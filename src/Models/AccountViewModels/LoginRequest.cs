using System.ComponentModel.DataAnnotations;

namespace GoldenTicket.Models.AccountViewModels
{
    public class LoginRequest
    {
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}