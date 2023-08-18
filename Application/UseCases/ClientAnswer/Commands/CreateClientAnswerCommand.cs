using Application.Common.Exceptions;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.DefaultAnswers.Commands;


public record CreateClientAnswerCommand(string AnswerText, Guid QuestionId, Guid DocId, Guid? DefaultAnswerId) : IRequest<Guid>;

public class CreateClientAnswerCommandHandler : IRequestHandler<CreateClientAnswerCommand, Guid>
{

    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateClientAnswerCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateClientAnswerCommand request, CancellationToken cancellationToken)
    {
        await FilterIfDocExsists(request.DocId);
        if ((request.AnswerText is null && request.DefaultAnswerId is null) ||
            (request.AnswerText is not null && request.DefaultAnswerId is not null))
        {
            throw new Exception(@" both answer and default answer id are null.
                Or both have values. one of them should be null and another is not null.
                    or reverse. ");
        }

        ClientAnswer clientAnswer = _mapper.Map<ClientAnswer>(request);

        clientAnswer.Id = Guid.NewGuid();
         await _dbContext.ClientAnswers.AddAsync(clientAnswer,cancellationToken);
         await _dbContext.SaveChangesAsync(cancellationToken);
        return clientAnswer.Id;
    }

    private async Task FilterIfDocExsists(Guid docId)
    {
        if (await _dbContext.Docs.FindAsync(docId) is null)
            throw new NotFoundException("There is no doc with given Id. ");
    }
}
