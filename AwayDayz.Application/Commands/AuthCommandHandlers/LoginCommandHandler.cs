using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwayDayz.Application.Commands.AuthCommands;
using AwayDayz.Application.Common;
using AwayDayz.Application.Interfaces;
using AwayDayz.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AwayDayz.Application.Commands.AuthCommandHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult<string>>
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<OperationResult<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.Username);

                if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                    return OperationResult<string>.Failure("Invalid username or password.");

                var token = await _tokenService.GenerateJwtToken(user);
                return OperationResult<string>.Success(token, "Login successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login failed: {ex.Message}");
                return OperationResult<string>.Failure("An unexpected error occured. Please try again later.");
            }
        }
    }
}
