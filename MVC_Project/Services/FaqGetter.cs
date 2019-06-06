using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;

namespace MVC_Project.Services
{
    public interface IFaqGetter
    { string[][] GetFaq(); }

    public class FaqGetter : IFaqGetter
    {
        IHostingEnvironment _host;
        IConfiguration _config;

        public FaqGetter(IHostingEnvironment hostEnv, IConfiguration config)
        {
            _host = hostEnv;
            _config = config;
        }

        public string[][] GetFaq()
        {
            string path = $"{_host.ContentRootPath}{_config.GetValue<string>("FaqFilePathFromRoot")}";
            return JsonConvert.DeserializeObject<string[][]>(File.ReadAllText(path));
        }
    }
}
