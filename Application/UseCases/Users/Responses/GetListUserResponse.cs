namespace Application.UseCases.Users.Responses;

public class GetListUserResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly Birthdate { get; set; }

    public string PhoneNumber { get; set; }

    public string Username { get; set; }

    public string UserTypeName { get; set; }

    public string QuarterName { get; set; }


}
