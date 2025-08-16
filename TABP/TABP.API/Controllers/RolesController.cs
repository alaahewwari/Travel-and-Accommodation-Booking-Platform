using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.Diagnostics;
using TABP.API.Common;
using TABP.API.Contracts.Rules;
using TABP.API.Extensions;
using TABP.API.Mappers;
using TABP.Application.Roles.Common;
using TABP.Application.Rules.Commands.Delete;
using TABP.Application.Rules.Queries.GetAll;
using TABP.Application.Rules.Queries.GetById;
namespace TABP.API.Controllers
{
    /// <summary>
    /// Provides endpoints for managing system rules, including creation, retrieval,
    /// update, and deletion operations. Only accessible to administrators.
    /// </summary>
    [ApiController]
    [OutputCache(Duration = 60)]
    public class RolesController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Creates a new rule.
        /// </summary>
        /// <param name="request">The rule data to create.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>The newly created rule.</returns>
        /// <response code="201">Rule created successfully.</response>
        /// <response code="400">Invalid input data.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">Admin privileges required.</response>
        /// <response code="500">Server error.</response>
        [HttpPost(ApiRoutes.Roles.Create)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoleResponse>> Create(
            [FromBody] CreateRoleRequest request,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand();
            var sw = Stopwatch.StartNew();
            var result = await mediator.Send(command, cancellationToken);
            sw.Stop();
            return result.ToCreatedResult(nameof(GetById), role => new { id = role.Id });
        }
        /// <summary>
        /// Retrieves a specific rule by its ID.
        /// </summary>
        /// <param name="id">The ID of the rule to retrieve.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>The rule details if found.</returns>
        /// <response code="200">Rule found and returned.</response>
        /// <response code="404">Rule not found.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">Admin privileges required.</response>
        /// <response code="500">Server error.</response>
        [HttpGet(ApiRoutes.Roles.GetById)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoleResponse>> GetById(
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            var query = new GetRoleByIdQuery(id);
            var result = await mediator.Send(query, cancellationToken);
            return result.ToActionResult();
        }
        /// <summary>
        /// Retrieves all rules in the system.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>A list of rules.</returns>
        /// <response code="200">Rules retrieved successfully.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">Admin privileges required.</response>
        /// <response code="500">Server error.</response>
        [HttpGet(ApiRoutes.Roles.GetAll)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(IEnumerable<RoleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RoleResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllRolesQuery();
            var result = await mediator.Send(query, cancellationToken);
            return result.ToActionResult();
        }
        /// <summary>
        /// Updates an existing rule.
        /// </summary>
        /// <param name="id">The ID of the rule to update.</param>
        /// <param name="request">The updated rule data.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>The updated rule details.</returns>
        /// <response code="200">Rule updated successfully.</response>
        /// <response code="400">Invalid input data.</response>
        /// <response code="404">Rule not found.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">Admin privileges required.</response>
        /// <response code="500">Server error.</response>
        [HttpPut(ApiRoutes.Roles.Update)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoleResponse>> Update(
            [FromRoute] int id,
            [FromBody] UpdateRoleRequest request,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            return result.ToActionResult();
        }
        /// <summary>
        /// Deletes a rule by its ID.
        /// </summary>
        /// <param name="id">The ID of the rule to delete.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>No content if deletion is successful.</returns>
        /// <response code="204">Rule deleted successfully.</response>
        /// <response code="404">Rule not found.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">Admin privileges required.</response>
        /// <response code="500">Server error.</response>
        [HttpDelete(ApiRoutes.Roles.Delete)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<object>> Delete(
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteRoleCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            return result.ToActionResult();
        }
    }
}