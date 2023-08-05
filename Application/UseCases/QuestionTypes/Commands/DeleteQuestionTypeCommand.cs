using Application.Common.Exceptions;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.QuestionTypes.Commands;


public record DeleteQuestionTypeCommand(Guid QuestionTypeId) : IRequest;
public class DeleteQuestionTypeCommandHandler : IRequestHandler<DeleteQuestionTypeCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteQuestionTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteQuestionTypeCommand request, CancellationToken cancellationToken)
    {
        var questionType = await _context.QuestionTypes.FindAsync(request.QuestionTypeId, cancellationToken);
        if (questionType is null)
        {
            throw new NotFoundException(nameof(QuestionType), request.QuestionTypeId);
        }
        _context.QuestionTypes.Remove(questionType);
        var result = await _context.SaveChangesAsync(cancellationToken);
    }
}
