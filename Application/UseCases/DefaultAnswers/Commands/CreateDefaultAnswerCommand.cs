using Application.Common.Exceptions;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.DefaultAnswers.Commands;


public record CreateDefaultAnswerCommand(string AnswerText, Guid QuestionId) : IRequest<Guid>;

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
       await  FilterIfQuestionExsists(request.QuestionId);
        DefaultAnswer answer = _mapper.Map<DefaultAnswer>(request);
        answer.Id = Guid.NewGuid();
        await _dbContext.DefaultAnswers.AddAsync(answer);
        await _dbContext.SaveChangesAsync();
        return answer.Id;
    }

    private async  Task FilterIfQuestionExsists(Guid questionId)
    {
        if (await _dbContext.Questions.FindAsync(questionId) is null)
            throw new NotFoundException("There is no question with given Id. ");

    }

}
