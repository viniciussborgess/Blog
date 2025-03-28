using Blog.API.Application.Models.Requests.Auth;
using Blog.API.Application.Models.ViewModels;
using Blog.API.Application.Queries.LoginQuery;
using Blog.Domain.Data.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blog.API.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _mediator.Send(new LoginQuery
                {
                    Nome = request.Nome,
                    Senha = request.Senha
                });

                var result = new LoginResponseViewModel
                {
                    Token = response.Token
                };

                return Ok(result);
            }
            catch (UserDomainException)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpGet("debug-auth")]
        public IActionResult DebugAuth()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized("Usuário não autenticado.");
            }

            return Ok(new
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                AuthenticationType = User.Identity.AuthenticationType,
                Claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList()
            });
        }
    }
}
