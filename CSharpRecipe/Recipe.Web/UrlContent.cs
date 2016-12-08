using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Web
{
    public class UrlContent
    {
        public static async Task<string> GetHtmlFromUrlAsync(Uri uri)
        {
            string html = string.Empty;
            HttpWebRequest request = HttpWebRequestTest.GenerateHttpWebRequest(uri);
            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            {
                if (ResponseStatusHandling.CategorizeResponse(response) == ResponseCategories.Success)
                {
                    Stream responseStream = response.GetResponseStream();

                    using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        html = reader.ReadToEnd();
                    }
                }
            }

            return html;
        }
    }
}
