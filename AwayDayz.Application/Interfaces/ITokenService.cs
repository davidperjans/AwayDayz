using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwayDayz.Domain.Entities;

namespace AwayDayz.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateJwtToken(User user);
    }
}
