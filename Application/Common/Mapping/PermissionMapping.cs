using Application.Common.Mapping.ValueResolvers;
using Application.Common.Models;
using Application.UseCases.Permissions.Commands.CreatePermission;
using Application.UseCases.Permissions.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class PermissionMapping : Profile
{
    public PermissionMapping()
    {
        _ = CreateMap<Permission, PermissionResponse>()
            .ForMember(x => x.PermissionId, o => o.MapFrom(x => x.Id))
           .ForMember(cr => cr.PermissionName, cfg => cfg
           .MapFrom<PermissionValueResolver<PermissionResponse>>());

        _ = CreateMap<CreatePermissionCommand, Permission>()
            .ForMember(x => x.PermissionName, o => o.MapFrom(y => y.permissions.First().TranslateText));

        _ = CreateMap<CreateCommandTranslate, TranslatePermission>();

        _ = CreateMap<UpdateCommandTranslate, TranslatePermission>();
    }
}
