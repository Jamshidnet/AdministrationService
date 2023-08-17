﻿using Application.Common.Mapping.ValueResolvers;
using Application.Common.Models;
using Application.UseCases.Categories.Responses;
using Application.UseCases.Questions.Commands;
using Application.UseCases.Questions.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class QuestionMapping : Profile
{
    public QuestionMapping()
    {
        _ = CreateMap<CreateQuestionCommand, Question>()
                .ForMember(y => y.QuestionText, q => q.MapFrom(t => t.questions.First().TranslateText));

        _ = CreateMap<UpdateQuestionCommand, Question>();

        _ = CreateMap<Question, GetListQuestionResponse>()
                .ForMember(x => x.QuestionType, y => y.MapFrom(z => z.QuestionType.QuestionTypeName))
                .ForMember(x => x.CreatorUser, y => y.MapFrom(z => z.CreatorUser.Username))
                .ForMember(x => x.Category, y => y.MapFrom(z => z.Category.CategoryName))
                .ForMember(cr => cr.QuestionText, cfg => cfg
                .MapFrom<QuestionValueResolver<GetListQuestionResponse>>());


        _ = CreateMap<Question, QuestionResponse>()
               .ForMember(x => x.CreatorUser, y => y.MapFrom(z => z.CreatorUser.Username))
                .ForMember(cr => cr.QuestionText, cfg => cfg
                .MapFrom<QuestionValueResolver<QuestionResponse>>());


        _ = CreateMap<Category, GetListCategoryResponse>()
                .ForMember(cr => cr.CategoryName, cfg => cfg
                .MapFrom<CategoryValueResolver<GetListCategoryResponse>>());


        _ = CreateMap<CreateCommandTranslate, TranslateQuestion>();

        _ = CreateMap<UpdateCommandTranslate, TranslateQuestion>();

    }
}
