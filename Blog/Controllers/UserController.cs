using Blog.API.Application.Commands.UserCommands.CreateUserCommand;
using Blog.API.Application.Commands.UserCommands.DeleteUserCommand;
using Blog.API.Application.Commands.UserCommands.UpdateUserCommand;
using Blog.API.Application.Models.Requests.User;
using Blog.API.Application.Models.ViewModels;
using Blog.API.Application.Models.ViewModels.Extensions;
using Blog.API.Application.Queries.UserQueries.GetAllUser;
using Blog.API.Application.Queries.UserQueries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blog.API.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpGet]
        [ProducesResponseType(typeof(UserViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var data = await _mediator.Send(new GetAllUserQuery());

            if (!data.Any())
            {
                return Ok(new List<UserViewModel>());
            }

            var result = data.Select(_ => _.ToViewModel()).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetByIdUserQuery { Id = id });

            if (result is null)
                return NotFound();
            return Ok(result.ToViewModel());
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateUserRequest request)
        {
            var operationResult = await _mediator.Send(new CreateUserCommand
            {
                Nome = request.Nome,
                Senha = request.Senha,
            });

            if (operationResult == Guid.Empty)
                return BadRequest();

            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateRequest request)
        {
            try
            {
                var isUpdated = await _mediator.Send(new UpdateUserCommand
                {
                    Id = id,
                    Nome = request.Nome,
                    Senha = request.Senha
                });

                if (isUpdated == false)
                    return BadRequest();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> Delete(Guid id)
        {

            var isDeleted = await _mediator.Send(new DeleteUserCommand { Id = id });

            if (isDeleted == false)
                return BadRequest();
            return Ok();
        }

    }
}
