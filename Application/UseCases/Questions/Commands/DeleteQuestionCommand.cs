using Application.Common.Exceptions;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Questions.Commands;


public record DeleteQuestionCommand(Guid QuestionId) : IRequest;
public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteQuestionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = await _context.Questions.FindAsync(request.QuestionId, cancellationToken);
        if (question is null)
        {
            throw new NotFoundException(nameof(Questions), request.QuestionId);
        }
         _context.Questions.Remove(question);
        var result = await _context.SaveChangesAsync(cancellationToken);
    }
}
