using Domain.Entities;

namespace Application.Interfaces
{
    public interface IResetPasswordRepository : IRepository<ResetPasswordToken>
    {
    }
}
