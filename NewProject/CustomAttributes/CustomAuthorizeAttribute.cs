using Domein.Common;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;


namespace NewProject.CustomAttributes
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        public PermissionTypes permission { get; set; }

        public void OnAuthorizationn(HttpActionContext actionContext)
        {

            if (actionContext.Response == null)
            {
                var token = actionContext.Request.Headers.Authorization?.Parameter;

                if (!string.IsNullOrEmpty(token))
                {
                    try
                    {
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var jwtToken = tokenHandler.ReadJwtToken(token);

                        var userPermissions = jwtToken.Claims
                            .Where(claim => claim.Type == "permission")
                            .Select(claim => (PermissionTypes)Enum.Parse(typeof(PermissionTypes), claim.Value))
                            .ToList();

                        if (!userPermissions.Contains(permission))
                        {
                            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "Unauthorized");
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid token");
                        return;
                    }

                }

                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Token not found");
                    return;
                }
            }
        }
    }
}
