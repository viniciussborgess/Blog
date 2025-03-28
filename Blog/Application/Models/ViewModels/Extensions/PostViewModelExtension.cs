using Blog.API.Application.Models.DTOs;

namespace Blog.API.Application.Models.ViewModels.Extensions;

public static class PostViewModelExtension
{
    public static PostViewModel ToViewModel(this PostDTO dto)
    {
        return new PostViewModel
        {
            Id = dto.Id,
            Titulo = dto.Titulo,
            Texto = dto.Texto,
            DataPostagem = dto.DataPostagem,
            UsuarioId = dto.UsuarioId
        };
    }

    public static IEnumerable<PostViewModel> ToViewModel(this IEnumerable<PostDTO> posts)
    {
        return posts.Select(x => x.ToViewModel());
    }
}
