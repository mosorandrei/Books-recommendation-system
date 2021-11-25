using System;
namespace Domain.Entities
{
    public class BookPoints
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public int Points { get; set; }
    }
}

