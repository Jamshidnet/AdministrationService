using Application.UseCases.Districts.Commands;
using Application.UseCases.Districts.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class DistrictMapping : Profile
{
    public DistrictMapping()
    {

        _ = CreateMap<CreateDistrictCommand, District>();
        _ = CreateMap<UpdateDistrictCommand, District>();
        _ = CreateMap<District, DistrictResponse>();
        _ = CreateMap<District, GetListDIstrictResponse>()
            .ForMember(x => x.RegionName, y => y.MapFrom(dis => dis.Region.RegionName));

    }

}
