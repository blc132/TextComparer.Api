using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TextComparer.Api.Dtos;
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
        [HttpPost]
        public ActionResult CompareText([FromBody] CompareTextDto dto)
        {
            var splittedTexts =_homeService.SplitTexts(dto.TextsToCompare, dto.SplitText);
            return Ok();
        }
    }
}