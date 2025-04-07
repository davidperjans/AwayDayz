using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwayDayz.Application.Common;
using MediatR;

namespace AwayDayz.Application.Commands.AuthCommands
{
    public class RegisterCommand : IRequest<OperationResult<string>>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


        public RegisterCommand(string username, string firstname, string lastname, string email, string password, string confirmPassword)
        {
            UserName = username;
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
    }
}
