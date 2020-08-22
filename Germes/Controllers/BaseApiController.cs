using Microsoft.AspNetCore.Mvc;

namespace Germes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : Controller
    {
    }
}
