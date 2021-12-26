using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.AuthModels
{
    public class FavouritesRequest
    {
        [Required(ErrorMessage = "UserId is required!")]
        [JsonProperty("UserId")]
        public string? UserId { get; set; }

        [Required(ErrorMessage = "BookId is required!")]
        [JsonProperty("BookId")]
        public Guid BookId { get; set; }
    }
}
