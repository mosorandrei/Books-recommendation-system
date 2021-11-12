

using Domain.Entities;
using Persistence.Context;
using System;
using System.Linq;

namespace Infrastructure.Test
{
    public class GenreDatabaseInitializer
    {
        public static void Initialize(GenreContext context)
        {
            if(context.Genres is null)
            {
                Seed(context);
                return;
            }

            if (context.Genres.Any())
            {
                return;
            }
        }

        private static void Seed(GenreContext context)
        {
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
        }
    }
}
