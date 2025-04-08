using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwayDayz.Application.Common;
using MediatR;

namespace AwayDayz.Application.Commands.Auth.AuthCommands
{
    public class LoginCommand : IRequest<OperationResult<string>>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
