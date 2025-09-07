using TodoListApp.Application.DTOs;
using TodoListApp.Domain.Entities;

namespace TodoListApp.Application.Interfaces
{
    public interface IItemServices
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item?> GetItemByIdAsync(Guid id);
        Task<Item> CreateItemAsync(CreateItemDto createItemDto);
        Task<bool> UpdateItemAsync(Guid id, UpdateItemDto updateItemDto);
        Task<bool> DeleteItemAsync(Guid id);
    }
}
