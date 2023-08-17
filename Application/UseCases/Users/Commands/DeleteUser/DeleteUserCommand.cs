using Application.Common.Exceptions;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid UserId) : IRequest;
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(request.UserId, cancellationToken)
           ?? throw new NotFoundException(nameof(Users), request.UserId);

        var person = await _context.People.FindAsync(user.PersonId)
            ?? throw new NotFoundException(" there is no person with this id ");

        _ = _context.Users.Remove(user);

        _ = _context.People.Remove(person);

        _ = await _context.SaveChangesAsync(cancellationToken);
    }
}
