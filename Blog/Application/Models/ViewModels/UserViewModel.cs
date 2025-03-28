namespace Blog.API.Application.Models.ViewModels;

public class UserViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public List<PostViewModel> Posts { get; set; }
}
