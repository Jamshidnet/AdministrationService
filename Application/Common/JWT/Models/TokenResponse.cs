using Application.UseCases.Permissions.Responses;

namespace NewProject.JWT.Models
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public List<PermissionResponse> Permissions { get; set; }
    }
}
