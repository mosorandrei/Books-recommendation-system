using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class BookGenreAssociation : BaseEntity
    {
        public Guid BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Book? Book { get; set; }
        public Guid GenreId { get; set; }
        [ForeignKey("GenreId")]
        public virtual Genre? Genre { get; set; }
    }
}
