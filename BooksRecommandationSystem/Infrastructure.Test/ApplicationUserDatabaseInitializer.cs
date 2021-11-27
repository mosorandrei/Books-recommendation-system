using Domain.Entities;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Test
{
    public class ApplicationUserDatabaseInitializer
    {
        public static void Initialize(ApplicationUserContext context)
        {
            if (context.Users is null)
            {
                Seed(context);
                return;
            }

            if (context.Users.Any())
            {
                return;
            }
        }

        private static void Seed(ApplicationUserContext context)
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
        }
    }
}
