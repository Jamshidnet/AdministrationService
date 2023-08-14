using Application.Common.Exceptions;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Languages.Commands;


public record DeleteLanguageCommand(Guid LanguageId) : IRequest;
public class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteLanguageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
    {
        var clientType = await _context.Languages.FindAsync(request.LanguageId, cancellationToken);
        if (clientType is null)
        {
            throw new NotFoundException(nameof(Language), request.LanguageId);
        }
        _context.Languages.Remove(clientType);
        var result = await _context.SaveChangesAsync(cancellationToken);
    }
}

