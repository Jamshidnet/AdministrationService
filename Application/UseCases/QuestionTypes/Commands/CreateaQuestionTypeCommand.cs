using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.QuestionTypes.Commands;
public record CreateQuestionTypeCommand(string QuestionTypeName) : IRequest<Guid>;

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
        questionType.Id = Guid.NewGuid();
        await _dbContext.QuestionTypes.AddAsync(questionType);
        await _dbContext.SaveChangesAsync();
        return questionType.Id;
    }
}
