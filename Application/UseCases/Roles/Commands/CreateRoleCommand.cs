using Application.Common.Abstraction;
using Application.Common.Models;
using AutoMapper;
using Domein.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Roles.Commands;

public record CreateRoleCommand(List<CreateCommandTranslate> roles, List<Guid> PermissionsIds) : IRequest<Guid>;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Guid>
{

    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;
    private readonly IMapper _mapper;

    public CreateRoleCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser, IMapper mapper)
    {
        _context = context;
        _currentUser = currentUser;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {


        var permissions = await _context.Permissions.ToListAsync(cancellationToken);

        var Newpermissons = new List<Permission>();
        if (request.PermissionsIds.Count > 0)
        {
            permissions.ForEach(p =>
            {
                if (request.PermissionsIds.Any(id => p.Id == id))
                    Newpermissons.Add(p);
            });

        }

        Role role = _mapper.Map<Role>(request);
        TranslateRole Trole = new();

        role.Id = Guid.NewGuid();

        request.roles.ForEach(c =>
        {
            Trole = _mapper.Map<TranslateRole>(c);
            Trole.OwnerId = role.Id;
            Trole.ColumnName = "RoleName";
            Trole.Id = Guid.NewGuid();
            _context.TranslateRoles
            .Add(Trole);
        });

        role.RoleName = request.roles.First().TranslateText;
            role.Permissions = Newpermissons;

        await _context.Roles.AddAsync(role, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return role.Id;
    }
}
