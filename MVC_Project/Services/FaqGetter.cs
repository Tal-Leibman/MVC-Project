using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.IO;

namespace MVC_Project.Services
{
    public interface IFaqGetter
    { string[][] GetFaq(); }

    public class FaqGetter : IFaqGetter
    {
        IHostingEnvironment _host;

        public FaqGetter(IHostingEnvironment hostEnv) => _host = hostEnv;

        public string[][] GetFaq()
        {
            string path = $"{_host.ContentRootPath}\\wwwroot\\faq.json";
            return JsonConvert.DeserializeObject<string[][]>(File.ReadAllText(path));
        }
    }
}
