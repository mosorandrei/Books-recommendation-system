using Domain.Constants;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.AuthModels
{
    public class UserReadingStatusesDtoFE
    {
        [Required(ErrorMessage = "Book is required!")]
        [JsonProperty("Book")]
        public BookDtoFE? Book { get; set; }
        [Required(ErrorMessage = "Status is required!")]
        [JsonProperty("Status")]
        public ReadingStatusEnum Status { get; set; }
        [Required(ErrorMessage = "IsFavourited is required!")]
        [JsonProperty("IsFavourited")]
        public bool IsFavourited { get; set; }
        [Required(ErrorMessage = "UserScore is required!")]
        [JsonProperty("UserScore")]
        public int UserScore { get; set; }
    }
}
