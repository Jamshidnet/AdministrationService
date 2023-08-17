using Application.UseCases.Logs.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class LogMapping : Profile
{
    public LogMapping()
    {
        _ = CreateMap<UserAction, UserActionResponse>();
        _ = CreateMap<DocChangeLog, DocChangeResponse>();
    }

}
