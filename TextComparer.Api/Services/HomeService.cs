using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextComparer.Api.Services.Interfaces;

namespace TextComparer.Api.Services
{
    public class HomeService : IHomeService
    {
        public IList<string> SplitTexts(string texts, string splitText)
        {
            return texts.Split(splitText);
        }
    }
}