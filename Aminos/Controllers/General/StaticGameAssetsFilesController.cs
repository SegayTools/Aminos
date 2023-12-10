using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aminos.Controllers.General;

[Route("api/General")]
[ApiController]
[Authorize]
public class StaticGameAssetsFilesController : ControllerBase
{
    
}