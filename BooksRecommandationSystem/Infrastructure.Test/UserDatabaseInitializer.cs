

using Domain.Entities;
using Persistence.Context;
using System;
using System.Linq;

namespace Infrastructure.Test
{
    public class UserDatabaseInitializer
    {
        public static void Initialize(UserContext context)
        {
            if (context.Users.Any())
            {
                return;
            }
            Seed(context);
        }

        private static void Seed(UserContext context)
        {
            var users = new[]
            {
                new User
                {
                    Id = Guid.Parse("e1b70054-f734-4761-8131-3442c8ffd057"),
                    Username = "Andrei",
                    Password = "*****"
                },
                new User
                {
                    Id = Guid.Parse("c7af7aae-cf4d-4e7e-891d-5f1adbfa83ed"),
                    Username = "Adrian",
                    Password = "*****"
                }

            };
            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
