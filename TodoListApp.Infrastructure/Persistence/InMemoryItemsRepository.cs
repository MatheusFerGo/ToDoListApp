using TodoListApp.Domain.Entities;
using TodoListApp.Domain.Interfaces;

namespace TodoListApp.Infrastructure.Persistence
{
    public class InMemoryItemsRepository : IItemsRepository
    {
        private readonly List<Item> _items = new List<Item>();

        public Task CreateAsync(Item item)
        {
            _items.Add(item);
            return Task.CompletedTask;
        }

        public Task<Item?> GetByIdAsync(Guid id)
        {
            var item = _items.FirstOrDefault(t => t.Id == id);
            return Task.FromResult(item);
        }

        public Task<IEnumerable<Item>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Item>>(_items);
        }

        public async Task UpdateAsync(Item newItem)
        {
            var existing_item = await GetByIdAsync(newItem.Id);
            if (existing_item is not null)
            {
                _items.Remove(existing_item);
                _items.Add(newItem);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await GetByIdAsync(id);
            if (item is not null) 
            {
                _items.Remove(item);
            }
        }
    }
}
