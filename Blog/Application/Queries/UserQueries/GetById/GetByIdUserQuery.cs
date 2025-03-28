using Blog.API.Application.Models.DTOs;
using MediatR;

namespace Blog.API.Application.Queries.UserQueries.GetById;

public class GetByIdUserQuery : IRequest<UserDTO>
{
    public Guid Id { get; set; }
}
