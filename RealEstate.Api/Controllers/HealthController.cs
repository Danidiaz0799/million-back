using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Api.Controllers
{
    /// <summary>
    /// Controller de prueba para verificar que la API está funcionando
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Verifica el estado de la API
        /// </summary>
        /// <returns>Estado de la API</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Status = "Healthy",
                Timestamp = DateTime.UtcNow,
                Version = "1.0.0",
                Message = "Real Estate API is running successfully"
            });
        }

        /// <summary>
        /// Endpoint de ping para verificar conectividad
        /// </summary>
        /// <returns>Pong response</returns>
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("Pong");
        }
    }
}