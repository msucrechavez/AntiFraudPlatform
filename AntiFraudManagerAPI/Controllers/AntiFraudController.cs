using Microsoft.AspNetCore.Mvc;

namespace AntiFraudManagerAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AntiFraudController : ControllerBase
{

    [HttpGet]
    [Route("/Status")]
    public string GetStatus()
    {
        return "running";
    }
}
