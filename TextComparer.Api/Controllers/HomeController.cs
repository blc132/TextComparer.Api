using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TextComparer.Api.Services.Interfaces;

namespace TextComparer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        // GET api/values
        [HttpPost("{textPattern, textsToCompare, splitText}")]
        public string[] CompareText(string textPattern, string textsToCompare, string splitText)
        {
            var splittedTexts =_homeService.SplitTexts(textsToCompare, splitText);
            return splittedTexts.ToArray();
        }
    }
}