using Application.Interfaces;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.v2
{
    public class ResetPasswordRepository : Repository<ResetPasswordToken>, IResetPasswordRepository
    {
        public ResetPasswordRepository(DatabaseContext context) : base(context) {
        
        }
    }
}
