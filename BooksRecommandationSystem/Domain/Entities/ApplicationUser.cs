using Microsoft.AspNetCore.Identity;
using System.Runtime.Serialization;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Uri? ImageUri { get; set; }
        public bool IsEnabled { get; set; }
        public int IsBlocked { get; set; } = 0;

        [IgnoreDataMember]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
