using Application.Common.Exceptions;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.DefaultAnswers.Commands;


public record DeleteDefaultAnswerCommand(Guid DefaultAnswerId) : IRequest;
public class DeleteDefaultAnswerCommandHandler : IRequestHandler<DeleteDefaultAnswerCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteDefaultAnswerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteDefaultAnswerCommand request, CancellationToken cancellationToken)
    {
        var answer = await _context.DefaultAnswers.FindAsync(request.DefaultAnswerId, cancellationToken);
        if (answer is null)
        {
            throw new NotFoundException(nameof(DefaultAnswers), request.DefaultAnswerId);
        }
         _context.DefaultAnswers.Remove(answer);
        var result = await _context.SaveChangesAsync(cancellationToken);
    }
}
