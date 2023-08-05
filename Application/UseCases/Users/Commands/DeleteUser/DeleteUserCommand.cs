using Application.Common.Exceptions;
using Domein.Entities;
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

        _context.Users.Remove(user);

        _context.People.Remove(person);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
