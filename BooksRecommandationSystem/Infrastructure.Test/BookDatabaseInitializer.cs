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
            if (context.Books is null)
            {
                Seed(context);
                return;
            }

            if (context.Books.Any())
            {
                return;
            }
        }

        private static void Seed(BookContext context)
        {
            var books = new[]
            {
                new Book
                {
                    Id = Guid.Parse("576c63eb-685f-4cd8-926b-de085c683a20"),
                    Title = "Divergent",
                    Rating = 10,
                    Description = "",
                    PublicationDate = DateTime.Now, //for testing purposes
                    ImageUri = new Uri("https://bit.ly/3xlV5i2"),
                    DownloadUri = new Uri("https://bit.ly/Reads_Divergent")
                },
                new Book
                {
                    Id = Guid.Parse("5ce76653-3bc0-40fd-8a04-661bcd551ca7"),
                    Title = "Lord of the Rings - The Fellowship of the Ring",
                    Rating = 9,
                    Description = "",
                    PublicationDate = DateTime.Now, //for testing purposes
                    ImageUri = new Uri("https://bit.ly/3cKkdpl"),
                    DownloadUri = new Uri("https://bit.ly/Reads_Lord")
                }
                  
            };
            if (context.Books is not null)
                context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}
