using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewRelic.Api.Agent;
using ProductosBFF.Domain.Parameters;
using ProductosBFF.Filters;
using ProductosBFF.Interfaces;
using ProductosBFF.Models.Commons;
using ProductosBFF.Models.Productos;
using System;
using System.Threading.Tasks;

namespace ProductosBFF.Controllers
{
    /// <summary>
    /// Controlador
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [UserAuthorizationFilter]
    public class PrestadoresController : ControllerBase
    {
        private readonly IPrestadoresService _prestadorService;
        private readonly ILogger<PrestadoresController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public PrestadoresController(IPrestadoresService prestadoresService, ILogger<PrestadoresController> logger)
        {
            _prestadorService = prestadoresService;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene los prestadores de un BC según su número de BC
        /// </summary>
        /// <param name="bc">número BC</param>
 
        [HttpGet("BC/{bc}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<PrestadoresDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        [Transaction(Web = true)]

        public async Task<ActionResult> GetPrestadores(long bc)
        {
            try
            {
                var prestadores = await _prestadorService.GetPrestadores(new BodyPrestadores() { PIN_BC = bc });

                if (prestadores == null)
                {
                    var notFoundObjectResult =
                        new GenericResult<PrestadoresDto>(null, 404, "No se encontraron prestadores");
                    return new NotFoundObjectResult(notFoundObjectResult);
                }

                var okObjectResult =
                    new GenericResult<PrestadoresDto>(prestadores, 200, "Se obtuvieron los prestadores correctamente");
                return new OkObjectResult(okObjectResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message);
            }
        }        
    }
}