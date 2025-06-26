using Domain.Enums;

namespace Application.DTO;

public class UpdateTaskItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    
}