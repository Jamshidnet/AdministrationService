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
        CreateMap<Permission, PermissionResponse>()
            .ForMember(x => x.PermissionId, o => o.MapFrom(x => x.Id))
           .ForMember(cr => cr.PermissionName, cfg => cfg
           .MapFrom<PermissionValueResolver<PermissionResponse>>());

        CreateMap<CreatePermissionCommand, Permission>()
            .ForMember(x => x.PermissionName, o => o.MapFrom(y => y.permissions.First().TranslateText));

        CreateMap<CreateCommandTranslate, TranslatePermission>();

        CreateMap<UpdateCommandTranslate, TranslatePermission>();
    }
}
