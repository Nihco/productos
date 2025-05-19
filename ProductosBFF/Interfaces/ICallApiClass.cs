using System.Threading.Tasks;

namespace ProductosBFF.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICallApiClass
    {
        /// <summary>
        /// Método que llama API via GET
        /// </summary>
        /// <typeparam name="T">Tipo que debe retornar</typeparam>
        /// <param name="appName">Nombre de la aplicacion en el appsetting.JSON</param>
        /// <param name="method">Método de la API</param>
        /// <param name="varIn">Objeto que tiene las variables de entrada, puede ser null</param>
        /// <returns></returns>
        public Task<T> GetApi<T>(string appName, string method, object varIn);
        
        /// <summary>
        /// Método que llama API via POST
        /// </summary>
        /// <typeparam name="T">Tipo que debe retornar</typeparam>
        /// <param name="appName">Nombre de la aplicacion en el appsetting.JSON</param>
        /// <param name="method">Método de la API</param>
        /// <param name="varIn">Objeto que tiene las variables de entrada, puede ser null</param>
        /// <returns></returns>
        public Task<T> PostApi<T>(string appName, string method, object varIn);

        /// <summary>
        /// Método que llama API via POST
        /// </summary>
        /// <typeparam name="T">Tipo que debe retornar</typeparam>
        /// <param name="appName">Nombre de la aplicacion en el appsetting.JSON</param>
        /// <param name="method">Método de la API</param>
        /// <param name="varIn">Objeto que tiene las variables de entrada, puede ser null</param>
        /// <returns></returns>
        public Task<T> PutApi<T>(string appName, string method, object varIn);
        /// <summary>
        /// Método que llama API via PATCH
        /// </summary>
        /// <typeparam name="T">Tipo que debe retornar</typeparam>
        /// <param name="appName">Nombre de la aplicacion en el appsetting.JSON</param>
        /// <param name="method">Método de la API</param>
        /// <param name="varIn">Objeto que tiene las variables de entrada, puede ser null</param>
        /// <returns></returns>
        public Task<string> PatchApi<T>(string appName, string method, object varIn);
        /// <summary>
        /// Método que llama API via DELETE
        /// </summary>
        /// <typeparam name="T">Tipo que debe retornar</typeparam>
        /// <param name="appName">Nombre de la aplicacion en el appsetting.JSON</param>
        /// <param name="method">Método de la API</param>
        /// <param name="varIn">Objeto que tiene las variables de entrada, puede ser null</param>
        /// <returns></returns>
        public Task<T> DeleteApi<T>(string appName, string method, object varIn);
    }
}