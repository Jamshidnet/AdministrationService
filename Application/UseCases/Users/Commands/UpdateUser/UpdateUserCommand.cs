using Application.Common.Exceptions;
using Application.Common.Extensions;
using AutoMapper;
using Domein.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime Birthdate { get; set; }

    public string PhoneNumber { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public Guid[] RoleIds { get; set; }

    public Guid LanguageId { get; set; }

    public Guid? QuarterId { get; set; }

    public Guid UserTypeId { get; set; }
}
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public UpdateUserCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var roles = await _context.Roles.ToListAsync(cancellationToken);
        var foundUser = await _context.Users.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(User), request.Id);

        if (request?.RoleIds?.Length > 0)
        {
            foundUser?.Roles?.Clear();
            roles.ForEach(role =>
            {
                if (request.RoleIds.Any(id => id == role.Id))
                    foundUser.Roles.Add(role);
            });

        }

        var person = await _context.People.SingleOrDefaultAsync(x => x.Id == foundUser.PersonId)
            ?? throw new NotFoundException(" There is no person with this id. ");

        _mapper.Map(request, person);
        _context.People.Update(person);
        foundUser.Username = request.Username;
        foundUser.UserTypeId = request.UserTypeId;
        foundUser.LanguageId = request.LanguageId;
        if (!string.IsNullOrEmpty(request.Password))
            foundUser.Password = (request.Password + foundUser.SaltId).GetHashedString();
        _context.Users.Update(foundUser);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
