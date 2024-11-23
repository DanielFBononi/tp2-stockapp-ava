using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StockApp.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        [HttpGet("dados")]
        public IActionResult GetDados()
        {
            return Ok(new { mensagem = "Somente admins podem acessar isso." });
        }
    }
}

