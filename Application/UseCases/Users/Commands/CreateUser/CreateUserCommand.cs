using Application.Common.Exceptions;
using Application.Common.Extensions;
using AutoMapper;
using Domein.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Users.Commands.CreateUser;

public record CreateUserCommand : IRequest<Guid>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime Birthdate { get; set; }

    public string PhoneNumber { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public Guid LanguageId { get; set; }
    public Guid[] RoleIds { get; set; }

    public Guid? QuarterId { get; set; }

    public Guid UserTypeId { get; set; }
}
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IApplicationDbContext context, IMapper mapper)
           => (_context, _mapper) = (context, mapper);




    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {

        if (_context.Users.Any(x => x.Username == request.Username))
            throw new AlreadyExistsException(nameof(User), request.Username);



        var roles = await _context.Roles.ToListAsync(cancellationToken);



        var userRole = new List<Role>();
        if (request?.RoleIds?.Length > 0)
            roles.ForEach(role =>
            {
                if (request.RoleIds.Any(id => id == role.Id))
                    userRole.Add(role);
            });

        Person person = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Birthdate = DateOnly.FromDateTime(request.Birthdate),
            PhoneNumber = request.PhoneNumber,
            QuarterId = request.QuarterId
        };
        person.Id = Guid.NewGuid();
        await _context.People.AddAsync(person);

        Guid salt = Guid.NewGuid();
        User user = new()
        {
            Id = Guid.NewGuid(),
            Password = (request.Password + salt).GetHashedString(),
            SaltId = salt,
            Username = request.Username,
            PersonId = person.Id,
            Roles = userRole,
            UserTypeId = request.UserTypeId,
            LanguageId = request.LanguageId
        };
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user.Id;


    }
}