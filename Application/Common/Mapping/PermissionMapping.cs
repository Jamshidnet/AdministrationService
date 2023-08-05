using Application.UseCases.Permissions.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class PermissionMapping : Profile
{
    public PermissionMapping()
    {
        CreateMap<Permission, PermissionResponse>()
            .ForMember(x => x.PermissionId, o => o.MapFrom(x => x.Id))
            .ForMember(x => x.PermissionName, o => o.MapFrom(x => x.PermissionName));
    }
}
