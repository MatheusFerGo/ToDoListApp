using Microsoft.EntityFrameworkCore;
using TodoListApp.Domain.Entities;

namespace TodoListApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        // Este construtor permite que nossa aplicação passe as configurãções de conexão
        // (Como a senha do banco de dados) para o Entity Framework.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Esta linha diz ao Entity Framework: "Eu quero que você gerencie uma tabela
        // que corresponserá à minha classe 'Items'". O EF Core vai, por padrão, 
        // nomear a tabela no plural ("Items")
        public DbSet<Item> Items { get; set; }
    }
}
