using Application.UseCases.QuestionTypes.Commands;
using Application.UseCases.QuestionTypes.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class QuestionTypeMapping : Profile
{
    public QuestionTypeMapping()
    {

        CreateMap<CreateQuestionTypeCommand, QuestionType>();
        CreateMap<UpdateQuestionTypeCommand, QuestionType>();
        CreateMap<QuestionType, QuestionTypeResponse>();
    }
}
