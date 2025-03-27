using AutoMapper;
using MediatR;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Application.DTOs.User;
using RealEstateApp.Application.Features.User.Queries;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.User.Handlers
{
    public class GetPagedUsersQueryHandler : IRequestHandler<GetPagedUsersQuery, ServiceResult<PagedResult<UserDto>>>
    {
        public readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetPagedUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResult<PagedResult<UserDto>>> Handle(GetPagedUsersQuery request, CancellationToken cancellationToken)
        {
            var pagedUsers = await _userRepository.GetAllAsync(request.Page, request.PageSize);

            return ServiceResult<PagedResult<UserDto>>.Success(pagedUsers);
        }
    }
}
