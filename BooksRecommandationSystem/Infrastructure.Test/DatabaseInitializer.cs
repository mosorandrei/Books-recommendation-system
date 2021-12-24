

using Domain.Entities;
using Persistence.Context;
using System;
using System.Linq;

namespace Infrastructure.Test
{
    public class DatabaseInitializer
    {
        public static void Initialize(DatabaseContext context)
        {
            if (context.Genres is null)
            {
                Seed(context);
                return;
            }

            if (context.Genres.Any())
            {
                return;
            }
        }

        private static void Seed(DatabaseContext context)
        {
            var users = new[]
            {
                new ApplicationUser
                {
                    FirstName = "Mihai",
                    LastName = "User1",
                    IsEnabled = false
                },
                new ApplicationUser
                {
                    FirstName = "Ion",
                    LastName = "User2",
                    IsEnabled = false
                }
            };
            if (context.Users is not null)
                context.Users.AddRange(users);
            context.SaveChanges();

            var genres = new[]
            {
                new Genre
                {
                    Id = Guid.Parse("407aaaa5-41cf-46ef-b352-66ad832cc9d2"),
                    Name = "Comedy"
                },
                new Genre
                {
                    Id = Guid.Parse("c6d9b6a1-b2eb-464f-9e17-07f41bd44d44"),
                    Name = "Action"
                }

            };
            if (context.Genres is not null)
                context.Genres.AddRange(genres);
            context.SaveChanges();

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

            var authors = new[]
            {
                new Author
                {
                    Id = Guid.Parse("90e191d1-88c3-4b34-a6ed-ac3aa8e51c4c"),
                    FirstName = "Mihai",
                    LastName = "Eminescu"
                },
                new Author
                {
                    Id = Guid.Parse("838d3b42-351d-494c-b65d-90cae5a919ef"),
                    FirstName = "Ion",
                    LastName = "Creanga"
                }
            };
            if (context.Authors is not null)
                context.Authors.AddRange(authors);
            context.SaveChanges();
        }
    }
}
