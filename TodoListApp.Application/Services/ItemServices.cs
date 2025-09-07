using TodoListApp.Application.DTOs;
using TodoListApp.Application.Interfaces;
using TodoListApp.Domain.Entities;
using TodoListApp.Domain.Interfaces;

namespace TodoListApp.Application.Services
{
    public class ItemServices : IItemServices
    {
        private readonly IItemsRepository _itemRepository;

        public ItemServices(IItemsRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<Item> CreateItemAsync(CreateItemDto createItemDto)
        {
            var item = new Item(
                createItemDto.Title,
                createItemDto.Description,
                createItemDto.DueDate);

            await _itemRepository.CreateAsync(item);
            return item;
        }

        public async Task<bool> UpdateItemAsync(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = await _itemRepository.GetByIdAsync(id);
            if (existingItem is null)
            {
                return false;
            }

            existingItem.Update(
                updateItemDto.Title,
                updateItemDto.Description,
                updateItemDto.DueDate);

            await _itemRepository.UpdateAsync(existingItem);
            return true;
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var existingItem = await _itemRepository.GetByIdAsync(id);
            if (existingItem is null)
            {
                return false;
            }

            await _itemRepository.DeleteAsync(id);
            return true;
        }

        public async Task<Item?> GetItemByIdAsync(Guid id)
        {
            return await _itemRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _itemRepository.GetAllAsync();
        }
    }
}