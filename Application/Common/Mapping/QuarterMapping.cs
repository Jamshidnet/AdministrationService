using Application.UseCases.Quarters.Commands;
using Application.UseCases.Quarters.Responses;
using AutoMapper;
using Domein.Entities;
using NewProject.Abstraction;

namespace Application.Common.Mapping;

public class QuarterMapping : Profile
{
    public QuarterMapping()
    {
      
        CreateMap<CreateQuarterCommand, Quarter>();
        CreateMap<UpdateQuarterCommand, Quarter>();
        CreateMap<Quarter, GetListQuarterResponse>()
            .ForMember(x => x.DistrictName, y => y.MapFrom(q => q.District.DistrictName));
        CreateMap<Quarter, QuarterResponse>();
         //   .ForMember(x => x.Clients, z => z.MapFrom(t => t.People.SingleOrDefault(y => y.QuarterId == t.Id).Clients));
    }
}
