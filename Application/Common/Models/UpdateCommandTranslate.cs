using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class UpdateCommandTranslate : CreateCommandTranslate
    {
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }
    }
}
