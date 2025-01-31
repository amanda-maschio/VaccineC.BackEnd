﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.UserResource;
using VaccineC.Query.Application.Queries.UserResource;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersResourcesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersResourcesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<UsersResourcesController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var command = new GetUserResourceListQuery();
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET api/<UsersResourcesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var command = new GetUserResourceByIdQuery(id);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET: api/<UsersResourcesController>/Info/GetByUser
        [HttpGet("{usersId}/GetByUser")]
        public async Task<IActionResult> GetByUser(Guid usersId)
        {
            try
            {
                var command = new GetUserResourceListByUserQuery(usersId);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/<UsersResourcesController>/Info/GetResourcesMenuByUser
        [HttpGet("{usersId}/GetResourcesMenuByUser")]
        public async Task<IActionResult> GetResourcesMenuByUser(Guid usersId)
        {
            try
            {
                var command = new GetUserResourceListMenuByUserQuery(usersId);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/<UsersResourcesController>/GetByUserResource
        [HttpPost, Route("GetByUserResource")]
        public async Task<IActionResult> GetByUserResource([FromBody] GetUserResourceByUserResourceQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<UsersResourcesController>/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] UserResourceViewModel userResource)
        {
            try
            {
                var command = new AddUserResourceCommand(userResource.ID, userResource.UsersId, userResource.ResourcesId, userResource.Register);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<UsersResourcesController>/UpdateUserResourceAccessNumber
        [HttpPost("{userId}/UpdateUserResourceAccessNumber")]
        public async Task<IActionResult> UpdateUserResourceAccessNumber(Guid userId, [FromBody] ResourceViewModel resource)
        {
            try
            {
                var command = new UpdateUserResourceAccessNumberCommand(userId, resource.UrlName);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<UsersResourcesController>/3/Update
        [HttpPut("{id}/Update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserResourceViewModel userResource)
        {
            try
            {
                var command = new UpdateUserResourceCommand(
                    id,
                    userResource.UsersId,
                    userResource.ResourcesId,
                    userResource.Register
                );
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(ex);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<UsersResourcesController>/3/Delete
        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeleteUserResourceCommand(id);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
