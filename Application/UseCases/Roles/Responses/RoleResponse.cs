using Application.UseCases.Permissions.Responses;

namespace Application.UseCases.Roles.Responses;

public class RoleResponse
{
    public Guid Id { get; set; }
    public string RoleName { get; set; }

    public List<PermissionResponse> Permissions { get; set; }
}
