using Domain.Common;
using Domain.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ReadingStatus : BaseEntity
    {
        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser? User { get; set; }
        public Guid BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Book? Book { get; set; }
        public ReadingStatusEnum Status { get; set; }
    }
}
