using System.Threading.Tasks;
using ProductosBFF.Models.SegundaClave;

namespace ProductosBFF.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApiSegundaClaveClient
    {
        /// <summary>
        /// Solciita 
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns></returns>
        public Task<SegundaClaveDto> SolicitarSegundaClaveAsync(SolicitudClaveDto solicitud);
        
        /// <summary>
        /// Verifica segunda clave
        /// </summary>
        /// <param name="verificacion"></param>
        /// <returns></returns>
        public Task<VerificadoDto> VerificarSegundaClaveAsync(VerificarSegundaClaveDto verificacion);
    }
}