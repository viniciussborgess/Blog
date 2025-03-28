namespace Blog.API.Application.Models.DTOs;

public class UserDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public List<PostDTO> Posts { get; set; }
}
