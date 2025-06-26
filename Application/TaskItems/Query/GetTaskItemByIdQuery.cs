using Application.DTO;
using Application.Interface;

namespace Application.TaskItems.Query;

public class GetTaskItemByIdQuery
{
    private readonly ITaskRepository _taskRepository;

    public GetTaskItemByIdQuery(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskItemResponse> GetTaskItemById(Guid taskId)
    {
        var Result = await _taskRepository.GetTaskItemByIdAsync(taskId);
        var Response = new TaskItemResponse()
        {
            IsThereError = false,
            Name = Result.Name,
            Description = Result.Description,
            Created = Result.Created,
            Status = Result.Status,
        };
        return Response;
    }
}