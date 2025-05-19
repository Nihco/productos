using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductosBFF.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHttpClientService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryParams"></param>
        /// <param name="headers"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetAsync<T>(string url, object queryParams = null, Dictionary<string, string> headers = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <param name="queryParams"></param>
        /// <param name="headers"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> PostAsync<T>(string url, object body = null, object queryParams = null, Dictionary<string, string> headers = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <param name="queryParams"></param>
        /// <param name="headers"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> PutAsync<T>(string url, object body = null, object queryParams = null, Dictionary<string, string> headers = null);
    }
}