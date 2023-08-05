using Application.UseCases.Districts.Responses;
using Domein.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Regions.Responses
{
    public  class RegionResponse
    {
        public Guid Id { get; set; }

        public string RegionName { get; set; }

        public virtual ICollection<DistrictResponse> Districts { get; set; } 
    }
}
