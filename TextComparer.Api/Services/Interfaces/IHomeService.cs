using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextComparer.Api.Dtos;

namespace TextComparer.Api.Services.Interfaces
{
    public interface IHomeService
    {
        string CompareText(CompareTextDto dto);
    }
}
