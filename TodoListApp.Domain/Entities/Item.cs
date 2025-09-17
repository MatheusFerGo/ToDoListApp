using TodoListApp.Domain.Enums;

namespace TodoListApp.Domain.Entities
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Title { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public ItemStatus Status { get; set; }
        public DateTime DueDate { get; private set; }
        public DateTime CreationTime { get; private set; }
        public DateTime? UpdateTime { get; private set; }

        public Item(string title, string? description, DateTime dueDate)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Status = ItemStatus.Pending;
            DueDate = dueDate;
            CreationTime = DateTime.UtcNow;
        }

        public void Update(string title, string? description, DateTime dueDate)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            UpdateTime = DateTime.UtcNow;
        }

        public void MarkAsCompleted()
        {
            if (Status == ItemStatus.Pending)
            {
                Status = ItemStatus.Completed;
                UpdateTime = DateTime.UtcNow;
            }
        }

        public void MarkAsPending()
        {
            if (Status == ItemStatus.Completed)
            {
                Status = ItemStatus.Pending;
                UpdateTime = DateTime.UtcNow;
            }
        }
    }
}