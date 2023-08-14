using Application.UseCases.Languages.Response;
using Application.UseCases.Quarters.Responses;
using Application.UseCases.Roles.Responses;
using Application.UseCases.UserTypes.Responses;

namespace Application.UseCases.Users.Responses;

public class UserResponse
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly Birthdate { get; set; }

    public string PhoneNumber { get; set; }

    public string Username { get; set; }

    public GetListUserTypeResponse UserType { get; set; }

    public GetListQuarterResponse Quarter { get; set; }

    public LanguageResponse Language { get; set; }

    public List<RoleResponse> Roles { get; set; }
}
