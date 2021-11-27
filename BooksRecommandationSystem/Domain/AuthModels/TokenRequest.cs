using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.AuthModels
{
    public class TokenRequest
    {
        /// <summary>
        /// The username of the user logging in.
        /// </summary>
        [Required(ErrorMessage = "User Email is required!")]
        [JsonProperty("email")]
        public string? Email { get; set; }

        /// <summary>
        /// The password for the user logging in.
        /// </summary>
        [Required(ErrorMessage = "User Password is required!")]
        [JsonProperty("password")]
        public string? Password { get; set; }
    }
}
