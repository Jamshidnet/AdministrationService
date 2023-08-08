using Application.UseCases.Districts.Commands;
using Application.UseCases.Districts.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class DistrictMapping : Profile
{
    public DistrictMapping()
    {

        CreateMap<CreateDistrictCommand, District>();
        CreateMap<UpdateDistrictCommand, District>();
        CreateMap<District, DistrictResponse>();
        CreateMap<District, GetListDIstrictResponse>()
            .ForMember(x => x.RegionName, y => y.MapFrom(dis => dis.Region.RegionName));

    }

}
