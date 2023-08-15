using Application.Common.Models;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.QuestionTypes.Commands;

public record CreateQuestionTypeCommand(List<CreateCommandTranslate> questionTypes) : IRequest<Guid>;

public class CreateQuestionTypeCommandHandler : IRequestHandler<CreateQuestionTypeCommand, Guid>
{
    private IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateQuestionTypeCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateQuestionTypeCommand request, CancellationToken cancellationToken)
    {
        QuestionType questionType = _mapper.Map<QuestionType>(request);
        TranslateQuestionType TquestionType = new();

        questionType.Id = Guid.NewGuid();

        request.questionTypes.ForEach(c =>
        {
            TquestionType = _mapper.Map<TranslateQuestionType>(c);
            TquestionType.OwnerId = questionType.Id;
            TquestionType.ColumnName = "QuestionTypeName";
            TquestionType.Id = Guid.NewGuid();
            _dbContext.TranslateQuestionTypes
            .Add(TquestionType);
        });

        await _dbContext.QuestionTypes.AddAsync(questionType);

        await _dbContext.SaveChangesAsync();
        return questionType.Id;
    }
}

