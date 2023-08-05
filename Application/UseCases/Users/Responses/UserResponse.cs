using Application.UseCases.Roles.Responses;


namespace Application.UseCases.Users.Responses;

public class UserResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly Birthdate { get; set; }

    public string PhoneNumber { get; set; }

    public string Username { get; set; }

    public string UserType { get; set; }
    
    public string QuarterName { get; set; }



    public List<RoleResponse> Roles { get; set; }
}
