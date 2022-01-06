using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class BookAuthorAssociation : BaseEntity
    {
        public Guid BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Book? Book { get; set; }
        public Guid AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual Author? Author { get; set; }
    }
}
