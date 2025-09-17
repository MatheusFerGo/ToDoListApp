// Infrastructure/DataModels/ItemPersistence.cs
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using TodoListApp.Domain.Enums;

namespace TodoListApp.Infrastructure.DataModels
{
    [Table("Items")] // Diz ao Supabase o nome da tabela
    public class ItemPersistence : BaseModel
    {
        [PrimaryKey("Id")] // A chave primária
        public Guid Id { get; set; }

        [Column("Title")]
        public string Title { get; set; } = string.Empty;

        [Column("Description")]
        public string? Description { get; set; }

        [Column("Status")]
        public ItemStatus Status { get; set; }

        [Column("DueDate")]
        public DateTime DueDate { get; set; }

        [Column("CreationTime")]
        public DateTime CreationTime { get; set; }

        [Column("UpdateTime")]
        public DateTime? UpdateTime { get; set; }
    }
}