using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.Services
{
    public interface IFaqGetter
    {
        string[][] GetFaq();
    }

    public class FaqGetter : IFaqGetter
    {
        string[][] _faq = new string[][]
        {
            new string[] { "How often do you work on this?", "Pretty often." },
            new string[] { "What is the airspeed velocity of an unladen swallow?", "African or European?" },
            new string[] { "how much wood could a woodchuck chuck if a woodchuck could chuck wood?", "At least 2." },
        };

        public string[][] GetFaq() => _faq;
    }
}
