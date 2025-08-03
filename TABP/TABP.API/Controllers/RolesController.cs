using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TABP.API.Common;
using TABP.API.Contracts.Rules;
using TABP.API.Mapping;
using TABP.Application.Roles.Common;
using TABP.Application.Rules.Commands.Delete;
using TABP.Application.Rules.Queries.GetAll;
using TABP.Application.Rules.Queries.GetById;
namespace TABP.API.Controllers
{
    /// <summary>
    /// Manages rule operations including creation, retrieval, updating, deletion,
    /// and fetching active rules.
    /// </summary>
    [ApiController]
    public class RolesController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Creates a new rule.
        /// </summary>
        [HttpPost(ApiRoutes.Roles.Create)]
        [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoleResponse>> Create([FromBody] CreateRoleRequest request, CancellationToken cancellationToken)
        {
            var command = request.ToCommand();
            var sw = Stopwatch.StartNew();
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        /// <summary>
        /// Retrieves a specific rule by its ID.
        /// </summary>
        [HttpGet(ApiRoutes.Roles.GetById)]
        [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoleResponse>> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetRoleByIdQuery(id);
            var result = await mediator.Send(query, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return Ok(result.Value);
        }

        /// <summary>
        /// Retrieves all rules.
        /// </summary>
        [HttpGet(ApiRoutes.Roles.GetAll)]
        [ProducesResponseType(typeof(IEnumerable<RoleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RoleResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllRolesQuery();
            var result = await mediator.Send(query, cancellationToken);
            return Ok(result.Value);
        }

        /// <summary>
        /// Updates an existing rule.
        /// </summary>
        [HttpPut(ApiRoutes.Roles.Update)]
        [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoleResponse>> Update([FromRoute] int id, [FromBody] UpdateRoleRequest request, CancellationToken cancellationToken)
        {
            var command = request.ToCommand(id);
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        /// <summary>
        /// Deletes a rule by its ID.
        /// </summary>
        [HttpDelete(ApiRoutes.Roles.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new DeleteRoleCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return NoContent();
        }
    }
}
