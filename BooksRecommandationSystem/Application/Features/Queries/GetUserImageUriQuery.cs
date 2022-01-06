using MediatR;

namespace Application.Features.Queries
{
    public class GetUserImageUriQuery : IRequest<Uri>
    {
        public string? UserId { get; set; }
    }
}
