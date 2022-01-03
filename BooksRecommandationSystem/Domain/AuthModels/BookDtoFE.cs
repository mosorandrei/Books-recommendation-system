using Domain.Entities;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.AuthModels
{
    public class BookDtoFE
    {
        [Required(ErrorMessage = "BookId is required!")]
        [JsonProperty("BookId")]
        public Guid BookId { get; set; }
        [Required(ErrorMessage = "Title is required!")]
        [JsonProperty("Title")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Rating is required!")]
        [JsonProperty("Rating")]
        public decimal Rating { get; set; }
        [Required(ErrorMessage = "NumberOfReviews is required!")]
        [JsonProperty("NumberOfReviews")]
        public int NumberOfReviews { get; set; }
        [Required(ErrorMessage = "Description is required!")]
        [JsonProperty("Description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "PublicationDate is required!")]
        [JsonProperty("PublicationDate")]
        public DateTime PublicationDate { get; set; }
        [Required(ErrorMessage = "UploadDate is required!")]
        [JsonProperty("UploadDate")]
        public DateTime UploadDate { get; set; }
        [Required(ErrorMessage = "ImageUri is required!")]
        [JsonProperty("ImageUri")]
        public Uri? ImageUri { get; set; }
        [Required(ErrorMessage = "DownloadUri is required!")]
        [JsonProperty("DownloadUri")]
        public Uri? DownloadUri { get; set; }
        public ICollection<Genre>? Genres { get; set; }
        public ICollection<Author>? Authors { get; set; }
    }
}
