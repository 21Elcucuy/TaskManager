using Application.DTO;
using Application.Interface;
using Domain.Entities;
using Domain.Enums;

namespace Application.TaskItems;

public class TaskItemServices :ITaskItemServices
{
    private readonly ITaskRepository _taskRepository;

    public TaskItemServices(ITaskRepository  taskRepository)
    {
        _taskRepository = taskRepository;
    }
    public async Task<TaskItemResponse> CreateTaskItem(AddTaskItem AddtaskItem ,string UserId)
    {
        var NewItem = new TaskItem()
        {
            Name = AddtaskItem.Name,
            Description = AddtaskItem.Description,
            Created = DateTime.Now,
            Status = Status.InProgress,
            UserId = UserId,
        };
        var response =  await _taskRepository.CreateAsync(NewItem);
        if (response is null)
        {
            return new TaskItemResponse()
            {
                IsThereError = true
            };
        }
        var ResponseItem = new TaskItemResponse()
        {
            IsThereError = false,
            Id  = response.Id,
            Name = AddtaskItem.Name,
            Description = AddtaskItem.Description,
            Created = DateTime.Now,
            Status = Status.InProgress,
        };
        return ResponseItem;
    }
    public async Task<TaskItemResponse> DeleteTaskItem(Guid id ,string UserId)
    {
        var Result = await _taskRepository.DeleteAsync(id,UserId);
        if (Result is null)
        {
            return new TaskItemResponse(){ IsThereError = true};
        }
        var Response = new TaskItemResponse()
        {
            IsThereError = false,
            Id = Result.Id,
            Name = Result.Name,
            Description = Result.Description,
            Created = Result.Created,
            Status = Result.Status,
           
        };
        return Response;
         
    }
    public async Task<TaskItemResponse> UpdateTaskItem(UpdateTaskItem updateTaskItem , string UserId)
    {
        var UpdatedItem = new TaskItem()
        {
            Id = updateTaskItem.Id,
            Name = updateTaskItem.Name,
            Description = updateTaskItem.Description,
            Status = updateTaskItem.Status,
            UserId =UserId
        };
        var Result =  await _taskRepository.UpdateAsync(UpdatedItem);
        if (Result is null)
        {
            return new TaskItemResponse()
            {
                IsThereError = true
            };
        }

        var respnse = new TaskItemResponse()
        {
            IsThereError = false,
            Id = Result.Id,
            Name = Result.Name,
            Description = Result.Description,
            Status = Result.Status,
            Created = Result.Created,
        };
        return respnse;
    }
    public async Task<List<TaskItemResponse>> GetAll(string UserId)
    {
        var result = await _taskRepository.GetAllAsync(UserId);
        var Response = new List<TaskItemResponse>();
        foreach (var task in result)
        {
            Response.Add(new TaskItemResponse()
                {
                    Id = task.Id,
                    Name = task.Name,
                    Description = task.Description,
                    IsThereError =false,
                    Created = task.Created,
                    Status = task.Status,
                }
            );
        }
        return Response;
    }
    public async Task<TaskItemResponse> GetTaskItemById(Guid taskId)
    {
        var Result = await _taskRepository.GetTaskItemByIdAsync(taskId);
        var Response = new TaskItemResponse()
        {
            Id = Result.Id,
            IsThereError = false,
            Name = Result.Name,
            Description = Result.Description,
            Created = Result.Created,
            Status = Result.Status,
        };
        return Response;
    }
}