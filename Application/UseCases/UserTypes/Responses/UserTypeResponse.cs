
using Application.UseCases.Users.Responses;

namespace Application.UseCases.UserTypes.Responses;

public class UserTypeResponse
{
    public Guid Id { get; set; }

    public string TypeName { get; set; }

    public virtual ICollection<UserResponse> Users { get; set; }
}
