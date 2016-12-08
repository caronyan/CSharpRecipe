using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Web
{
    public class HttpWebRequestTest
    {
        public static HttpWebRequest GenerateHttpWebRequest(Uri uri)
        {
            HttpWebRequest httpRequest = (HttpWebRequest) WebRequest.Create(uri);
            return httpRequest;
        }

        public static HttpWebRequest GenerateHttpWebRequest(Uri uri, string postData, string contentType)
        {
            HttpWebRequest httpRequest = GenerateHttpWebRequest(uri);

            byte[] bytes = Encoding.UTF8.GetBytes(postData);
            httpRequest.ContentType = contentType;
            httpRequest.ContentLength = postData.Length;

            using (Stream requeStream=httpRequest.GetRequestStream())
            {
                requeStream.Write(bytes, 0, bytes.Length);
            }

            return httpRequest;
        }
    }
}
