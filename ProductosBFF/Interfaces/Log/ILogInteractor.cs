using System.Threading.Tasks;

namespace ProductosBFF.Interfaces.Log
{
    /// <summary>
    /// Interfaz para la interacción con los logs.
    /// </summary>
    public interface ILogInteractor
    {
        /// <summary>
        /// Método para insertar un registro en la tabla de log.
        /// </summary>
        /// <param name="descripcion">Descripción de la acción realizada.</param>
        /// <param name="accion">Tipo de acción realizada.</param>
        /// <param name="user">Usuario que realizó la acción.</param>
        /// <returns>Tarea que representa la operación asincrónica.</returns>
        Task InsertLog(string descripcion, string accion, string user);
    }
}
