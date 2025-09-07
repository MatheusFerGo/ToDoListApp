using TodoListApp.Domain.Entities;

namespace TodoListApp.Domain.Interfaces
{
    public interface IItemsRepository
    {
        Task CreateAsync(Item item);
        Task<Item?> GetByIdAsync(Guid id);
        Task<IEnumerable<Item>> GetAllAsync();
        Task UpdateAsync(Item item);
        Task DeleteAsync(Guid id);
    }
}