using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Authority;

namespace WebApiDemo.Controllers
{
    [ApiController]
    public class AuthorityController:ControllerBase
    {
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] AppCredential credential)
        {
            if (ApplicationRepository.Authenticate(credential.ClientId, credential.Secret))
            {
                return Ok(new
                {
                    access_token = CreateToken(credential.ClientId),
                    expiresAt = DateTime.UtcNow.AddMinutes(10)
                }); 
            }
        
            else
            {
                ModelState.AddModelError("unauthorized", "You are not authorized");
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status401Unauthorized
                };
               return new UnauthorizedObjectResult(problemDetails);
            }
        }
        public string CreateToken(string clientId)
        {
            return clientId;
        }
    }

   
}
