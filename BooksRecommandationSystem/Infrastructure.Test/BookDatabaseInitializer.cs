using Domain.Entities;
using Persistence.Context;
using System;
using System.Linq;


namespace Infrastructure.Test
{
    public class BookDatabaseInitializer
    {
        public static void Initialize(BookContext context)
        {
            if (context.Books.Any())
            {
                return;
            }
            Seed(context);
        }

        private static void Seed(BookContext context)
        {
            var books = new[]
            {
                new Book
                {
                    Id = Guid.Parse("576c63eb-685f-4cd8-926b-de085c683a20"),
                    Title = "Titanic",
                    Rating = 10,
                    Description = "",
                    PublicationDate = DateTime.Now,//for testing purposes
                    ImageUri = new Uri("https://en.wikipedia.org/wiki/Titanic")
                },
                new Book
                {
                    Id = Guid.Parse("5ce76653-3bc0-40fd-8a04-661bcd551ca7"),
                    Title = "Titanic2",
                    Rating = 9,
                    Description = "",
                    PublicationDate = DateTime.Now,//for testing purposes
                    ImageUri = new Uri("https://en.wikipedia.org/wiki/Titanic2") 
                }
                  
            };
            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}
