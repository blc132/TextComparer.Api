using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
            var test = String.CompareOrdinal(dto.TextPattern, dto.TextsToCompare);
            var comp = dto.TextPattern.Equals(dto.TextsToCompare);

            var result = _homeService.CompareText(dto);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}