using Application.UseCases.Clients.Commands;
using Application.UseCases.Clients.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public  class ClientMapping : Profile
{
    public ClientMapping()
    {
        CreateMap<CreateClientCommand, Client>();
        CreateMap<UpdateClientCommand, Client>();
        CreateMap<Client, ClientResponse>()
            .ForMember(d => d.PhoneNumber, cfg => cfg.MapFrom(ent => ent.Person.PhoneNumber))
            .ForMember(d => d.FirstName, cfg => cfg.MapFrom(ent => ent.Person.FirstName))
            .ForMember(d => d.LastName, cfg => cfg.MapFrom(ent => ent.Person.LastName))
            .ForMember(d => d.Birthdate, cfg => cfg.MapFrom(ent => ent.Person.Birthdate))
            .ForMember(d => d.Quarter, cfg => cfg.MapFrom(ent => ent.Person.Quarter.QuarterName))
            .ForMember(d => d.ClientType, cfg => cfg.MapFrom(ent => ent.ClientType.TypeName));
        CreateMap<ClientResponse, Client>();
    }
}
