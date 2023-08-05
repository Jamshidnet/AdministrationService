using Application.UseCases.Permissions.Responses;
using AutoMapper;
using Domein.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Roles.Responses
{
    public  class RoleResponse
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }

        public List<PermissionResponse> Permissions { get; set; }
    }
}
