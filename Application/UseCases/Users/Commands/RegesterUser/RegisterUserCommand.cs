using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.UseCases.Roles.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;
using NewProject.JWT.Interfaces;
using NewProject.JWT.Models;

namespace Application.UseCases.Users.Commands.RegesterUser;

public class RegisterUserCommand : IRequest<TokenResponse>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime Birthdate { get; set; }

    public string PhoneNumber { get; set; }

    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public Guid? QuarterId { get; set; }

    public Guid UserTypeId { get; set; }
}
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, TokenResponse>
{
    private readonly IJwtToken _jwtToken;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;


    public RegisterUserCommandHandler(IJwtToken jwtToken, IApplicationDbContext context, IMapper mapper)
    {
        _jwtToken = jwtToken;
        _context = context;
        _mapper = mapper;
    }
    public async Task<TokenResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {

        if (_context.Users.Any(x => x.Username == request.Username))
            throw new AlreadyExistsException(nameof(User), request.Username);

        var user = _mapper.Map<User>(request);
        var person = _mapper.Map<Person>(request);

        user.SaltId = Guid.NewGuid();
        user.Password = (user.Password+user.SaltId).GetHashedString();
        user.Id = Guid.NewGuid();
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.People.AddAsync(person, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        var tokenResponse = _jwtToken.CreateTokenAsync(user.Username, user.Id.ToString(), new List<RoleResponse>(), cancellationToken);
        return tokenResponse;
    }
}