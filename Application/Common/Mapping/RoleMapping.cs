using Application.UseCases.Roles.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public  class RoleMapping : Profile
{
    public RoleMapping()
    {
        CreateMap<Role, RoleResponse>();
        CreateMap<Role, GetListRoleResponse>();
    }
}
