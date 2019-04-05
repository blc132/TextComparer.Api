using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextComparer.Api.Services.Interfaces
{
    interface IHomeService
    {
        IList<string> SplitTexts(string texts, string splitText);
    }
}
