using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.AuthModels
{
    public class RefreshTokenDto
    {
        [Required(ErrorMessage = "Token is required!")]
        [JsonProperty("token")]
        public string? Token { get; set; }

        [Required(ErrorMessage = "Expire time is required!")]
        [JsonProperty("expireTime")]
        public long ExpireTime { get; set; }
    }
}
