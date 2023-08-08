using Application.UseCases.Clients.Responses;

namespace Application.UseCases.ClientTypes.Responses
{
    public class ClientTypeResponse
    {
        public Guid Id { get; set; }

        public string TypeName { get; set; }

        public virtual ICollection<GetListClientResponse> Clients { get; set; }

    }
}
