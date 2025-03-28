using Blog.API.Application.Models.DTOs;
using MediatR;

namespace Blog.API.Application.Queries.PostQueries.GetAllPost;

public class GetAllPostQuery : IRequest<IEnumerable<PostDTO>>
{
}
