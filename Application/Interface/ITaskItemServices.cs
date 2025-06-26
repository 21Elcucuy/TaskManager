using Application.DTO;

namespace Application.Interface;

public interface ITaskItemServices
{
    public Task<TaskItemResponse> CreateTaskItem(AddTaskItem AddtaskItem ,string UserId);
    public Task<TaskItemResponse> DeleteTaskItem(Guid id ,string UserId);
    public Task<TaskItemResponse> UpdateTaskItem(UpdateTaskItem updateTaskItem,string UserId);
    public Task<List<TaskItemResponse>> GetAll(string UserId);
    public Task<TaskItemResponse> GetTaskItemById(Guid taskId);
}