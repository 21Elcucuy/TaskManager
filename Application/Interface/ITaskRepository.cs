
using Application.DTO;
using Domain.Enums;
using Domain.Entities;
namespace Application.Interface;

public interface ITaskRepository
{
     
    public Task<TaskItem> CreateAsync(TaskItem item);
    public Task<TaskItem> UpdateAsync(TaskItem item);
    public Task<TaskItem> DeleteAsync(Guid id , string UserId);
    public Task<IEnumerable<TaskItem>> GetAllAsync(string UserId);
    public Task<TaskItem> GetTaskItemByIdAsync(Guid taskId);
    
}