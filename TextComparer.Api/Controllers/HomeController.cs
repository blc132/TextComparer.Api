using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace TextComparer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        // GET api/values
        [HttpPost("{textPattern, textsToCompare, splitText}")]
        public string CompareText(string textPattern, string textsToCompare, string splitTexts)
        {
            return textPattern + " " + textsToCompare + " " + splitTexts;
        }
    }
}