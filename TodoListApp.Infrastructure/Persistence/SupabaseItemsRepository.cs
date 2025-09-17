using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListApp.Domain.Entities;
using TodoListApp.Domain.Interfaces;
using TodoListApp.Infrastructure.DataModels;

namespace TodoListApp.Infrastructure.Persistence
{
    public class SupabaseItemsRepository : IItemsRepository
    {
        private readonly Supabase.Client _supabase;

        public SupabaseItemsRepository(Supabase.Client supabase)
        {
            _supabase = supabase;
        }

        public async Task CreateAsync(Item item)
        {
            var persistenceModel = ToPersistenceModel(item);
            await _supabase.From<ItemPersistence>().Insert(persistenceModel);
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            var response = await _supabase.From<ItemPersistence>().Get();
            return response.Models.Select(ToDomainEntity);
        }

        public async Task<Item?> GetByIdAsync(Guid id)
        {
            var response = await _supabase.From<ItemPersistence>()
                .Filter("Id", Supabase.Postgrest.Constants.Operator.Equals, id.ToString()) // <-- Garanta que está MAIÚSCULO
                .Get();

            var model = response.Models.FirstOrDefault();

            return model != null ? ToDomainEntity(model) : null;
        }

        public async Task UpdateAsync(Item item)
        {
            var persistenceModel = ToPersistenceModel(item);
            await _supabase.From<ItemPersistence>().Update(persistenceModel);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _supabase.From<ItemPersistence>()
                .Filter("Id", Supabase.Postgrest.Constants.Operator.Equals, id.ToString()) // <-- Garanta que está MAIÚSCULO
                .Delete();
        }

        // --- Mapeadores Privados (continuam iguais) ---
        private ItemPersistence ToPersistenceModel(Item entity)
        {
            return new ItemPersistence 
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Status = entity.Status,
                DueDate = entity.DueDate,
                CreationTime = entity.CreationTime,
                UpdateTime = entity.UpdateTime
            };
        }
        private Item ToDomainEntity(ItemPersistence model)
        {
            return new Item(model.Title, model.Description, model.DueDate) { Id = model.Id, Status = model.Status };
        }
    }
}