using Blog.API.Application.Commands.PostCommands.CreatePostCommand;
using Blog.API.Application.Commands.PostCommands.DeletePostCommand;
using Blog.API.Application.Commands.PostCommands.UpdatePostCommand;
using Blog.API.Application.Models.Requests.Post;
using Blog.API.Application.Models.ViewModels;
using Blog.API.Application.Models.ViewModels.Extensions;
using Blog.API.Application.Queries.PostQueries.GetAllPost;
using Blog.API.Application.Queries.PostQueries.GetByIdPost;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Blog.API.Controllers;

[Authorize]
[Route("api/v1/post")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(PostViewModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get()
    {
        var data = await _mediator.Send(new GetAllPostQuery());

        if (!data.Any())
        {
            return NoContent();
        }

        var result = data.Select(_ => _.ToViewModel()).ToList();

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PostViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var data = await _mediator.Send(new GetByIdPostQuery { Id = id });

        if (data is null)
            return NotFound();

        return Ok(data.ToViewModel());
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreatePostRequest request)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        if (userId == null) return Unauthorized();

        var operationResult = await _mediator.Send(new CreatePostCommand
        {
            UsuarioId = Guid.Parse(userId),
            Titulo = request.Titulo,
            Texto = request.Texto,
        });

        if (operationResult == Guid.Empty)
            return BadRequest();

        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        if (userId == null) return Unauthorized();

        var operationResult = await _mediator.Send(new DeletePostCommand
        {
            Id = id
        });

        if (!operationResult)
            return NotFound();

        return Ok();
    }

    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePostRequest request)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        if (userId == null) return Unauthorized();

        try
        {
            var operationResult = await _mediator.Send(new UpdatePostCommand
            {
                Id = id,
                UsuarioId = Guid.Parse(userId),
                Titulo = request.Titulo,
                Texto = request.Texto
            });

            if (!operationResult)
                return BadRequest();

        }
        catch (NullReferenceException)
        {
            return NotFound();
        }

        return Ok();
    }

}
