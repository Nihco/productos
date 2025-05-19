using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductosBFF.Domain.Parameters;
using ProductosBFF.Domain.Universal;
using ProductosBFF.Interfaces.Universal;
using System;
using System.Threading.Tasks;

namespace ProductosBFF.Controllers
{

    /// <summary>
    /// Controlador
    /// </summary>
    [Route("api/[controller]")]
    //[ServiceFilter(typeof(ValidaHeaderAuthorizationFilter))]
    [ApiController]
    public class UniversalController : ControllerBase
    {
        private readonly IUniversalInteractor _universalInteractor;
        private readonly ILogger<UniversalController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="universalInteractor"></param>
        /// <param name="logger"></param>
        public UniversalController(IUniversalInteractor universalInteractor, ILogger<UniversalController> logger)
        {
            _universalInteractor = universalInteractor;
            _logger = logger;
        }

        /// <summary>
        ///  Ingresar Accidente
        /// </summary>
        /// <returns></returns>
        [HttpPost("IngresoUniversal")]
        [ProducesResponseType(typeof(IngresoUniversalNSD), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> IngresoUniversal([FromBody] IngresoUniversal ingresoUniversal)
        {
            try
            {
                var ingrAccidente = await _universalInteractor.IngresoUniversal(ingresoUniversal);

                if (ingrAccidente == null)
                {
                    return new NotFoundObjectResult("No se ingreso el accidente correctamente");
                }
                return new OkObjectResult(ingrAccidente);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "No se pudo procesar la solicitud " + ex.InnerException);
            }

        }

    }
}
