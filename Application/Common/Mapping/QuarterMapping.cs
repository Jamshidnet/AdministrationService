using Application.UseCases.Quarters.Commands;
using Application.UseCases.Quarters.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class QuarterMapping : Profile
{
    public QuarterMapping()
    {

        _ = CreateMap<CreateQuarterCommand, Quarter>();
        _ = CreateMap<UpdateQuarterCommand, Quarter>();
        _ = CreateMap<Quarter, GetListQuarterResponse>()
            .ForMember(x => x.DistrictName, y => y.MapFrom(q => q.District.DistrictName));
        _ = CreateMap<Quarter, QuarterResponse>();
        //   .ForMember(x => x.Clients, z => z.MapFrom(t => t.People.SingleOrDefault(y => y.QuarterId == t.Id).Clients));
    }
}
