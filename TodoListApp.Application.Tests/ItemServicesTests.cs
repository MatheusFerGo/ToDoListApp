using Moq;
using TodoListApp.Application.DTOs;
using TodoListApp.Application.Services;
using TodoListApp.Domain.Entities;
using TodoListApp.Domain.Interfaces;

namespace TodoListApp.Application.Tests
{
    public class ItemServiceTests
    {
        [Fact]
        public async Task CreateItemAsync_MustCallRepositoriesOneTime()
        {
            //Arrange

            // 1. Criar um "mock" (uma simulação) do nosso repositório.
            // Ele vai se comportar exatamente como dissermos para ele se comportar.
            var mockRepo = new Mock<IItemsRepository>();

            // 2. Criar uma instância do nosso serviço, mas passando o mock em vez do repositório real.
            // O serviço não saberá a diferença, ele só precisa de algo que implemente IItemsRepository
            var itemService = new ItemServices(mockRepo.Object);

            // 3. Criar os dados de entrada para criar o método que vamos testar.
            var createItemDto = new CreateItemDto("Comprar pão", "Na padaria da esquina", DateTime.UtcNow.AddDays(1));

            // Act
            // 4. Executar o método que queremos testar.
            await itemService.CreateItemAsync(createItemDto);

            // Assert (Verificar)

            // 5. Verificar se o nosso objetivo foi alcançado.
            // Neste caso, queremos garantir que o método 'CreateAsync' do repositório
            // foi chamado exatamente UMA VEZ. Isso prova que nosso serviço está
            // delegando a tarefa de salvar para a camada de infraestrutura.
            mockRepo.Verify(mockRepo => mockRepo.CreateAsync(It.IsAny<Item>()), Times.Once);
        }

        [Fact]
        public async Task UpdateItemAsync_MustReturnFalseForInvalidUpdateId()
        {
            // Arrange
            
            // 1. Crias os dados de entrada.
            var itemIdNotCreated = Guid.NewGuid();
            var updateItemDto = new UpdateItemDto("Novo Título", "Nova Descrição", DateTime.UtcNow.AddDays(1));

            // 2. Criar o mock do repositório.
            var mockRepo = new Mock<IItemsRepository>();

            // 3. Configurar o mock
            // Estamos dizendo ao Moq: "Quando o método GetByIdAsync for chamado com QUALQUER Guid,
            // simule que você não encontrou nada, retornando um valor nulo."
            mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Item)null);

            // 4. Criar a instância do serviço com o mock configurado.
            var itemService = new ItemServices(mockRepo.Object);

            // Act

            // 5. Executar o método de atualização, passando o ID que sabemos que "não existe.
            var result = await itemService.UpdateItemAsync(itemIdNotCreated, updateItemDto);

            // 6 Verificar se o resultado do método foi 'false', como o esperado.
            Assert.False(result);

            // 7. Verificar também se o método de atualizar do repositório nunca foi chamado.
            // Isso garante que não estamos tentando salvar no banco de dados desnecessariamente.
            mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Item>()), Times.Never);

        }

        [Fact]
        public async Task DeleteItemAsync_MustReturnFalseForDeleteInvalidId()
        {
            var invalidId = Guid.NewGuid();

            var mockRepo = new Mock<IItemsRepository>();

            mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Item)null);

            var itemService = new ItemServices(mockRepo.Object);

            var result = await itemService.DeleteItemAsync(invalidId);

            Assert.False(result);
        }
    }
}