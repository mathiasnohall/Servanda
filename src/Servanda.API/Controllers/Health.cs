using Microsoft.AspNet.Mvc;


namespace Servanda.API.Controllers
{
    [Route("api/[controller]")]
    public class Health : Controller
    {
        // GET: api/values
        [HttpGet]
        public string Get()
        {
            return "OK";
        }
    }
}
