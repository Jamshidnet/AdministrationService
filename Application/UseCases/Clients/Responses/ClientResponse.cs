using Application.UseCases.ClientTypes.Responses;
using Application.UseCases.DefaultAnswers.Responses;
using Application.UseCases.Docs.Responses;
using Application.UseCases.Quarters.Responses;
using Domein.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Clients.Responses
{
    public class ClientResponse
    {
            public Guid Id { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public DateOnly Birthdate { get; set; }

            public string PhoneNumber { get; set; }

            public GetListQuarterResponse Quarter { get; set; }

            public GetListClientTypeResponse ClientType { get; set; }

        public virtual ICollection<GetListDocResponse> Docs { get; set; }


    }
}
