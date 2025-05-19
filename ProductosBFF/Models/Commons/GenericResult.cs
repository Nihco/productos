using System.Collections.Generic;

namespace ProductosBFF.Models.Commons
{
    /// <summary>
    /// GenericResponse
    /// </summary>
    public class GenericResult<T>
    {
        /// <summary>
        /// GenericResult Constructor
        /// </summary>
        /// <param name="result"></param>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <param name="errors"></param>
        public GenericResult(T result, int statusCode, string message, List<string> errors = null)
        {
            Result = result;

            bool successfulOperation = false;

            if (statusCode >= 200 && statusCode <= 299)
            {
                successfulOperation = true;
            }

            Response = new GenericResponse
            {
                SuccessfulOperation = successfulOperation,
                StatusCode = statusCode,
                Message = message,
                Errors = errors
            };
        }

        /// <summary>
        /// Result
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// Response
        /// </summary>
        public GenericResponse Response { get; set; }
    }
}
