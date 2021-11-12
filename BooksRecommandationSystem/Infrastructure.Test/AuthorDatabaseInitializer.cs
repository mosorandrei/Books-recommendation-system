using Domain.Entities;
using Persistence.Context;
using System;
using System.Linq;

namespace Infrastructure.Test
{
    public class AuthorDatabaseInitializer
    {
        public static void Initialize(AuthorContext context)
        {
            if(context.Authors is null)
            {
                Seed(context);
                return;
            }

            if(context.Authors.Any())
            {
                return;
            }
        }

        private static void Seed(AuthorContext context)
        {
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
