﻿using Application.Interfaces;
using Domain.AuthModels;
using MediatR;

namespace Application.Features.Queries
{
    public class GetAdminsQueryHandler : IRequestHandler<GetAdminsQuery, IEnumerable<ApplicationUserDTO>>
    {
        private readonly ITokenRepository repository;

        public GetAdminsQueryHandler(ITokenRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IEnumerable<ApplicationUserDTO>> Handle(GetAdminsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAllAdminsAsync();
        }
    }
}
