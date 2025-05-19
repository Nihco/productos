using System;
using System.Text;
using System.Web;

namespace ProductosBFF.Utils
{
    /// <summary>
    /// Clase
    /// </summary>
    public static class StringMethods
    {
        /// <summary>
        /// Capitalizar string
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string Capitalize(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return string.Empty;
            }
            else if (word.Length == 1)
            {
                return word[0].ToString();
            }
            else
            {
                return word[0].ToString().ToUpper() + word.Substring(1).ToString().ToLower();
            }
        }

        /// <summary>
        /// Decode
        /// </summary>
        /// <param name="encoded"></param>
        /// <returns></returns>
        public static string Decode(string encoded)
        {
            return HttpUtility.HtmlDecode(encoded);
        }

        /// <summary>
        /// Decode
        /// </summary>
        /// <param name="encoded"></param>
        /// <returns></returns>
        public static string Format(string encoded)
        {
            var data = Encoding.GetEncoding("iso-8859-1").GetBytes(encoded);
            return Encoding.UTF8.GetString(data);
        }

        /// <summary>
        /// GetPathAndQuery
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetPathAndQuery(string url)
        {
            var uri = new Uri(url);
            return uri.PathAndQuery.Substring(1, uri.PathAndQuery.Length - 1);
        }
    }
}
