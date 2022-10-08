using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

[ApiController, Route("api/whoami")]
public class WhoamiController : ControllerBase
{
  [HttpGet]
  public async Task<ActionResult> Get()
  {
    return Ok(new
    {
      User = Request.Headers["Remote-User"].FirstOrDefault(),
      Groups = Request.Headers["Remote-Groups"].FirstOrDefault(),
      Name = Request.Headers["Remote-Name"].FirstOrDefault(),
      Email = Request.Headers["Remote-Email"].FirstOrDefault(),
    });
  }
}
