using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Common;
using TABP.API.Contracts.User;
using TABP.API.Mapping;
namespace TABP.API.Controllers
{
    [Route(ApiRoutes.Base)]
    [ApiController]
    public class IdentityController(ISender mediator, IdentityMapper mapper) : ControllerBase
    {
        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
        {
            var command = mapper.ToCommand(request);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error.Description);
            }
            return Ok(result.Value);
        }
        [HttpGet("test")]
        [Authorize]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)] //not work with authorize because auth adds Cache-Control: no-store).
        public IActionResult Test()
        {
            return Ok("Test successful!");
        }
    }
}