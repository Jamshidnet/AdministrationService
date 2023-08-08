using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserTypes.Responses
{
    public class GetListUserTypeResponse
    {
        public Guid Id { get; set; }

        public string TypeName { get; set; }
    }
}
