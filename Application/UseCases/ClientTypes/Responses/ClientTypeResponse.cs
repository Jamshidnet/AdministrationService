using Application.UseCases.Clients.Responses;
using Domein.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.ClientTypes.Responses
{
    public class ClientTypeResponse
    {
        public Guid Id { get; set; }

        public string TypeName { get; set; }

        public virtual ICollection<ClientResponse> Clients { get; set; }

    }
}
