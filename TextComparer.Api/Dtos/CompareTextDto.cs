using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextComparer.Api.Dtos
{
    public class CompareTextDto
    {
        public string TextPattern { get; set; }
        public string TextsToCompare { get; set; }
        public string SplitText { get; set; }
    }
}
