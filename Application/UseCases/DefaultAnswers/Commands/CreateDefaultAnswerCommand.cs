using Application.Common.Exceptions;
using Application.Common.Models;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.DefaultAnswers.Commands;


public record CreateDefaultAnswerCommand(List<CreateCommandTranslate> defaultAnswers, Guid QuestionId) : IRequest<Guid>;

public class CreateDefaultAnswerCommandHandler : IRequestHandler<CreateDefaultAnswerCommand, Guid>
{
    private IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateDefaultAnswerCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateDefaultAnswerCommand request, CancellationToken cancellationToken)
    {
        await FilterIfQuestionExsists(request.QuestionId);
        DefaultAnswer defaultAnswer = _mapper.Map<DefaultAnswer>(request);
        TranslateDefaultAnswer TdefaultAnswer = new();

        defaultAnswer.Id = Guid.NewGuid();

        request.defaultAnswers.ForEach(c =>
        {
            TdefaultAnswer = _mapper.Map<TranslateDefaultAnswer>(c);
            TdefaultAnswer.OwnerId = defaultAnswer.Id;
            TdefaultAnswer.ColumnName = "DefaultAnswerName";
            TdefaultAnswer.Id = Guid.NewGuid();
            _dbContext.TranslateDefaultAnswers
            .Add(TdefaultAnswer);
        });

        await _dbContext.DefaultAnswers.AddAsync(defaultAnswer);

        await _dbContext.SaveChangesAsync();
        return defaultAnswer.Id;
    }

    private async Task FilterIfQuestionExsists(Guid questionId)
    {
        if (await _dbContext.Questions.FindAsync(questionId) is null)
            throw new NotFoundException("There is no question with given Id. ");

    }

}
