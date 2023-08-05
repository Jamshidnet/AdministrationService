using Application.UseCases.Permissions.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Permissions.Commands.CreatePermission;

public class CreatePermissionCommand : IRequest<List<PermissionResponse>>
{
    public string[] Name { get; set; }
}
public class CreatePermissionCommandHanler : IRequestHandler<CreatePermissionCommand, List<PermissionResponse>>
{
    private IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreatePermissionCommandHanler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<PermissionResponse>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {

        var _permissions = new List<Permission>();

        foreach (string item in request.Name)
        {
            _permissions.Add(new()
            {
                Id=Guid.NewGuid(),
                PermissionName = item
            });
        }

        await _dbContext.Permissions.AddRangeAsync(_permissions, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        var result = _mapper.Map<List<PermissionResponse>>(_permissions);
        return result;
    }
}