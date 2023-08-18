using Application.Common.Exceptions;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.ClientAnswers.Commands;


public record DeleteClientAnswerCommand(Guid ClientAnswerId) : IRequest;
public class DeleteClientAnswerCommandHandler : IRequestHandler<DeleteClientAnswerCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteClientAnswerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteClientAnswerCommand request, CancellationToken cancellationToken)
    {
        var answer = await _context.ClientAnswers.FindAsync(request.ClientAnswerId, cancellationToken)
            ?? throw new NotFoundException(nameof(ClientAnswers), request.ClientAnswerId);
        
         _context.ClientAnswers.Remove(answer);
        var result = await _context.SaveChangesAsync(cancellationToken);
    }
}
