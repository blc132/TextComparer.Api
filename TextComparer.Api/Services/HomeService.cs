using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Localization.Internal;
using Newtonsoft.Json;
using TextComparer.Api.Dtos;
using TextComparer.Api.Infrastructure;
using TextComparer.Api.Services.Interfaces;

namespace TextComparer.Api.Services
{
    public class HomeService : IHomeService
    {
        private readonly string _bufferPath;

        public HomeService(IHostingEnvironment env)
        {
            _bufferPath = Path.Combine(env.WebRootPath, "../Content/buffer.txt");
        }

        public string CompareText(CompareTextDto dto)
        {
            var patternText = RemoveNewLineCharacters(dto.TextPattern);
            var textsToCompare = RemoveNewLineCharacters(dto.TextsToCompare);
            var texts = textsToCompare.Split(dto.SplitText);
            
            SaveFile(patternText, texts);
            var response = SendFileToApache();
            var convertedResponse = ConvertResponse(response);
            return convertedResponse;
        }

        private void SaveFile(string textPattern, string[] texts)
        {
            if (File.Exists(_bufferPath))
                File.Delete(_bufferPath);

            using (StreamWriter sw = File.CreateText(_bufferPath))
            {
                sw.WriteLine(textPattern);
                foreach (var text in texts)
                {
                    sw.WriteLine(text);
                }
            }
        }

        private string SendFileToApache()
        {
            byte[] response;
            string stringResponse;

            //send file to apache
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/octet-stream");
                using (Stream fileStream = File.OpenRead(_bufferPath))
                response = client.UploadData("http://192.168.0.125:42069", fileStream.ToArray());
                stringResponse = client.Encoding.GetString(response);
            }
            return stringResponse;
        }

        private string ConvertResponse(string response)
        {
            var percentageResults = Regex.Matches(response, @"[0-9]?[0-9]?[0-9]?\.[0-9]?[0-9]?").Select(x => x.Value).ToList();
            var responseText = "";
            for (var i = 1; i < percentageResults.Count; i++)
            {
                responseText += "Tekst nr." + i + " jest podobny w " + percentageResults[i] + "% \n";
            }
            return responseText;
        }

        private string RemoveNewLineCharacters(string text)
        {
            return text.Replace("\n\n\n", String.Empty).Replace("\n\n", String.Empty).Replace("\n", String.Empty); 
        }
    }
}