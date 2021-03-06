using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.AuthModels
{
    public class ApplicationUserDto
    {
        [Required(ErrorMessage = "ID is required!")]
        [JsonProperty("id")]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Username is required!")]
        [JsonProperty("username")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "User Email is required!")]
        [JsonProperty("email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "ImageUri is required!")]
        [JsonProperty("ImageUri")]
        public Uri? ImageUri { get; set; }

        [Required(ErrorMessage = "First Name is required!")]
        [JsonProperty("firstname")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required!")]
        [JsonProperty("lastname")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "IsBlocked is required!")]
        [JsonProperty("isblocked")]
        public int IsBlocked { get; set; }
    }
}
