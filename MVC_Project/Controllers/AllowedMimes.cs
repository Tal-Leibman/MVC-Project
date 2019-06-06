using Microsoft.Extensions.Configuration;

namespace MVC_Project.Services
{
    public interface IAllowedMimes
    {
        string GetAllowed();
    }

    public class AllowedMimes : IAllowedMimes
    {
        IConfiguration _config;

        public AllowedMimes(IConfiguration config) => _config = config;

        public string GetAllowed() => _config.GetValue<string>("AcceptedUploadMIMEs");
    }
}
