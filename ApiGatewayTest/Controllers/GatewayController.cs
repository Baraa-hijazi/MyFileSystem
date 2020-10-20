using Microsoft.AspNetCore.Mvc;

namespace ApiGateway
{
    public class GatewayController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}