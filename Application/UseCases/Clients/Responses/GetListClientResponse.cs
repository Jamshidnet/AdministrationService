namespace Application.UseCases.Clients.Responses
{
    public class GetListClientResponse
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateOnly Birthdate { get; set; }

        public string PhoneNumber { get; set; }

        public string Quarter { get; set; }

        public string ClientType { get; set; }

    }
}
