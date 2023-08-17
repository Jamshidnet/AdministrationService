using Application.Common.Abstraction;
using Application.Common.Exceptions;
using Application.UseCases.Roles.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Roles.Commands;

public class UpdateRoleCommand : IRequest<RoleResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Guid> PermissionsIds { get; set; }
}

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, RoleResponse>
{

    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _userService;

    public UpdateRoleCommandHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService userService)
    {
        _context = context;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<RoleResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var permissions = await _context.Permissions.ToListAsync(cancellationToken);
        var role = await FilterIfRoleExsists(request.Id);

        if (request?.PermissionsIds?.Count >= 0)
        {
            //role.Permissions.Clear();
            permissions.ForEach(p =>
            {
                if (request.PermissionsIds.Any(id => p.Id == id))
                    role.Permissions.Add(p);
            });
        }

        var transRole = await _context.TranslateRoles
             .FirstOrDefaultAsync(x => x.OwnerId == role.Id
                                  && x.LanguageId.ToString() == _userService.LanguageId);

        transRole.TranslateText = request.Name;
        _ = _context.Roles.Update(role);
        _ = _context.TranslateRoles.Update(transRole);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<RoleResponse>(role);
    }

    private async Task<Role> FilterIfRoleExsists(Guid Id)
    {
        return await _context.Roles.FindAsync(Id)
            ?? throw new NotFoundException(" There is no role with this id. ");

    }

}