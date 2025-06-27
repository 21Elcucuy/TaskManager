using Domain.Enums;

namespace Application.DTO;

public class TaskItemResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public Status Status { get; set; }
    public bool IsThereError { get; set; }
}