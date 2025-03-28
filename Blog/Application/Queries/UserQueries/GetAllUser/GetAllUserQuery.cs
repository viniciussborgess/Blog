using Blog.API.Application.Models.DTOs;
using MediatR;

namespace Blog.API.Application.Queries.UserQueries.GetAllUser;

public class GetAllUserQuery : IRequest<IEnumerable<UserDTO>>
{
}
