using Application.Common.Models;
using Application.UseCases.Clients.Commands;
using Application.UseCases.ClientTypes.Commands;
using Application.UseCases.ClientTypes.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping
{
    public class ClientTypeMapping : Profile
    {

        public ClientTypeMapping()
        {
            CreateMap<CreateClientTypeCommand, ClientType>()
                .ForMember(x => x.TypeName, y => y.MapFrom(y => y.clientTypes.First().TranslateText));

            CreateMap<UpdateClientCommand, ClientType>();
            CreateMap<ClientType, ClientTypeResponse>();
            CreateMap<ClientType, GetListClientTypeResponse>();

            CreateMap<CreateCommandTranslate, TranslateClientType>();

            CreateMap<UpdateCommandTranslate, TranslateClientType>();


        }

    }
}
