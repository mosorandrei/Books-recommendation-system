using Domain.Common;

namespace Domain.Entities
{
    public class ResetPasswordToken : BaseEntity
    {
        public string? Email { get; set; }
        public int Token { get; set; }
    }
}
