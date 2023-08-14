using Application.UseCases.Clients.Commands;
using Application.UseCases.Languages.Commands;
using Application.UseCases.Languages.Response;
using AutoMapper;
using Domein.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mapping;

public class LanguageMapping : Profile
{

    public LanguageMapping()
    {
        CreateMap<CreateLanguageCommand, Language>();
        CreateMap<Language, LanguageResponse>();
    }

}
