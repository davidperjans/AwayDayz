using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AwayDayz.Application.Commands.Auth.AuthCommands;
using AwayDayz.Application.Common;
using AwayDayz.Application.Interfaces;
using AwayDayz.Application.Services;
using AwayDayz.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AwayDayz.Application.Commands.Auth.AuthCommandHandlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, OperationResult<string>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<OperationResult<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var doesUserExist = await _userManager.FindByNameAsync(request.UserName);
                if (doesUserExist != null)
                    return OperationResult<string>.Failure("Username already exists.");

                var newUser = _mapper.Map<User>(request);

                var result = await _userManager.CreateAsync(newUser, request.Password);
                if (!result.Succeeded)
                {
                    return OperationResult<string>.Failure($"Registration failed");
                }

                // Assign default role to the user
                await _userManager.AddToRoleAsync(newUser, "User");

                return OperationResult<string>.Success("Registration successful.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login failed: {ex.Message}");
                return OperationResult<string>.Failure("An unexpected error occured. Please try again later.");
            }
        }
    }
}
