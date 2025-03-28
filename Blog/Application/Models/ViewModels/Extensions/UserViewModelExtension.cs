using Blog.API.Application.Models.DTOs;

namespace Blog.API.Application.Models.ViewModels.Extensions;

public static class UserViewModelExtension
{
    public static UserViewModel ToViewModel(this UserDTO dto)
    {
        return new UserViewModel
        {
            Id = dto.Id,
            Nome = dto.Nome,
            Posts = dto.Posts.ToViewModel().ToList()
        };
    }

    public static IEnumerable<UserViewModel> ToViewModel(this IEnumerable<UserDTO> dto)
    {
        return dto.Select(x => x.ToViewModel());
    }
}
