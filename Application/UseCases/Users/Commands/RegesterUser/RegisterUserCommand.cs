using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.UseCases.Users.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public Guid LanguageId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

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
        person.Id = Guid.NewGuid();
        var roles = await _context.Roles.ToListAsync();
        var NewUserRole = roles.SingleOrDefault(x => x.RoleName == "NewUser");
        user.Roles.Add(NewUserRole);
        user.SaltId = Guid.NewGuid();
        user.Password = (user.Password + user.SaltId).GetHashedString();
        user.Id = Guid.NewGuid();
        user.Person = person;
        user.UserType = await _context.UserTypes.SingleOrDefaultAsync(x => x.TypeName == "NoneSet");
        // user.LanguageId = request.LanguageId;
        _ = await _context.Users.AddAsync(user, cancellationToken);
        _ = await _context.People.AddAsync(person, cancellationToken);

        user.Language = _context.Languages.Find(user.LanguageId);
        _ = await _context.SaveChangesAsync(cancellationToken);

        UserResponse userResponse = _mapper.Map<UserResponse>(user);
        var tokenResponse = _jwtToken.CreateTokenAsync(userResponse, cancellationToken);

        return tokenResponse;
    }
}