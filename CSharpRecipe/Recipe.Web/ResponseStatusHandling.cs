using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Web
{
    public enum ResponseCategories
    {
        Unknown,
        Informational,
        Success,
        Redirected,
        ClientError,
        ServerError
    }

    public class ResponseStatusHandling
    {
        public static ResponseCategories CategorizeResponse(HttpWebResponse httpResponse)
        {
            int statusCode = (int) httpResponse.StatusCode;
            if (statusCode>=100&&statusCode<=199)
            {
                return ResponseCategories.Informational;
            }
            else if (statusCode >= 200 && statusCode <= 299)
            {
                return ResponseCategories.Success;
            }
            else if (statusCode >= 300 && statusCode <= 399)
            {
                return ResponseCategories.Redirected;
            }
            else if (statusCode >= 400 && statusCode <= 499)
            {
                return ResponseCategories.ClientError;
            }
            else if (statusCode >= 500 && statusCode <= 599)
            {
                return ResponseCategories.ServerError;
            }

            return ResponseCategories.Unknown;
        }
    }
}
