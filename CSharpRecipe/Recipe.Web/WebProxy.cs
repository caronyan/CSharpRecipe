using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Web
{
    public class WebProxyTest
    {
        public static HttpWebRequest AddProxyToRequest(HttpWebRequest httpRequest, Uri proxyUri, string proxyId,
            string proxyPassword, string proxyDomain)
        {
            if (httpRequest == null)
            {
                throw new ArgumentNullException(nameof(httpRequest));
            }

            //Proxy object
            WebProxy proxyInfo = new WebProxy();
            //Proxy address
            proxyInfo.Address = proxyUri;
            //Local bypass setting
            proxyInfo.BypassProxyOnLocal = true;
            proxyInfo.Credentials = new NetworkCredential(proxyId, proxyPassword, proxyDomain);
            httpRequest.Proxy = proxyInfo;

            return httpRequest;

        }
    }
}
