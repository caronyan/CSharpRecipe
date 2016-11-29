using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.DataType
{
    static class DataTypeExtMethods
    {
        public static string Base64EncodeBytes(this byte[] inputBytes) => (Convert.ToBase64String(inputBytes));

        public static byte[] Base64DecodeString(this string inputStr) => (Convert.FromBase64String(inputStr));
    }
}
