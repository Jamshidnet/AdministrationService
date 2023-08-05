namespace Domein.Entities.Identity;

public class UserRefreshToken
{
    public string RefreshToken { get; set; }
    public string UserName { get; set; }
    public DateTime ExpiresTime { get; set; }
}
