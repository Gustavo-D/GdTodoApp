using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GdToDoApp.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/")]
    public class MyBaseController : ControllerBase
    {
        
    }
}
