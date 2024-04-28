using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using social_media_app.Repository;

namespace social_media_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactController : ControllerBase
    {
        IReactRepository reactRepository;
    }
}
