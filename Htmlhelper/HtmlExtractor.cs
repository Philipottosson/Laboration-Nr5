using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;
using System.Windows;

namespace Htmlhelper
{
    public class HtmlExtractor
    {
        //public List<string> imageURL { get; }
        public async Task<List<string>> UrlLink(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string html = await client.GetStringAsync(url);



            List<string> listValues = SeparateValues(html, url);
            //List<byte> listByte = separateByte(responseByte);

            return listValues;
        }
        public List<string> SeparateValues(string s, string url)//byte s) 
        {
            List<string> list = new List<string>();
            string regexCheckimg = @"<img[^>]*?src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?>";
            MatchCollection collection = Regex.Matches(s,regexCheckimg,RegexOptions.IgnoreCase|RegexOptions.Singleline);
            foreach (Match item in collection)
            {
                string temp = item.Groups[1].Value;
                if (temp.Contains("http"))
                {
                    list.Add(temp);
                }
                else list.Add(url + temp);
            }
            return list;
        }
        
    }
}
