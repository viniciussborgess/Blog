using Blog.API.Application.Models.DTOs;
using MediatR;

namespace Blog.API.Application.Queries.PostQueries.GetByIdPost;

public class GetByIdPostQuery : IRequest<PostDTO>
{
    public Guid Id { get; set; }
}
