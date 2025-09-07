namespace TodoListApp.Application.DTOs
{
    public record CreateItemDto(string Title, string? Description, DateTime DueDate);
}
