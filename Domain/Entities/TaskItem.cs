using Domain.Enums;

namespace Domain.Entities;

public class TaskItem
{  
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public Status Status { get; set; }
    
    public string UserId { get; set; }
    
}