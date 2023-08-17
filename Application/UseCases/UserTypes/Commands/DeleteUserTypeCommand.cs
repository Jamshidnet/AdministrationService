using Application.Common.Exceptions;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.UserTypes.Commands;


public record DeleteUserTypeCommand(Guid UserTypeId) : IRequest;
public class DeleteUserTypeCommandHandler : IRequestHandler<DeleteUserTypeCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteUserTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteUserTypeCommand request, CancellationToken cancellationToken)
    {
        var clientType = await _context.UserTypes.FindAsync(request.UserTypeId, cancellationToken);
        if (clientType is null)
        {
            throw new NotFoundException(nameof(UserType), request.UserTypeId);
        }
        _ = _context.UserTypes.Remove(clientType);
        var result = await _context.SaveChangesAsync(cancellationToken);
    }
}
