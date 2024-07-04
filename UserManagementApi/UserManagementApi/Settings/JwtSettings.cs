// Settings/JwtSettings.cs
namespace UserManagementApi.Settings
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int TokenLifetime { get; set; }
    }
}
