using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Localization.Internal;
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
            _bufferPath = Path.Combine(env.WebRootPath, "Content/buffer.txt");
        }

        public string CompareText(CompareTextDto dto)
        {
            var texts = dto.TextsToCompare.Split(dto.SplitText);
            SaveFile(dto.TextPattern, texts);
            return  SendFileToApache();
        }

        private void SaveFile(string textPattern, string[] texts)
        {
            if (File.Exists(_bufferPath))
                File.Delete(_bufferPath);

            //create txt, txt pattern:
            //textPattern
            //textToCompare1
            //...
            //textToCompareN
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
                response = client.UploadData("http://192.168.0.102:42069", fileStream.ToArray());
                stringResponse = client.Encoding.GetString(response);
            }

            return stringResponse;
        }
    }
}