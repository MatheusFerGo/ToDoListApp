namespace TodoListApp.Application.DTOs
{
    public record UpdateItemDto(string Title, string? Description, DateTime DueDate);
}
