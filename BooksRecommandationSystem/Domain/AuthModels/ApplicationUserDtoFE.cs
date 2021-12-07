using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.AuthModels
{
    public class ApplicationUserDtoFE
    {
        [Required(ErrorMessage = "UserId is required!")]
        [JsonProperty("userid")]
        public string? UserId { get; set; }

        [Required(ErrorMessage = "Full name is required!")]
        [JsonProperty("fullname")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [JsonProperty("email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "isAdmin is required!")]
        [JsonProperty("isAdmin")]
        public int IsAdmin { get; set; }
    }
}
