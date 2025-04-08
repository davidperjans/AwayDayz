using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AwayDayz.Application.Commands.Auth.AuthCommands;
using AwayDayz.Domain.Entities;

namespace AwayDayz.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterCommand, User>();
        }
    }
}
