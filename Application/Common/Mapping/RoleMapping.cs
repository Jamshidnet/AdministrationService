using Application.UseCases.Roles.Responses;
using AutoMapper;
using Domein.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mapping
{
    public  class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<Role, RoleResponse>();
        }
    }
}
