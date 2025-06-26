// using Application.DTO;
// using Application.Interface;
// using Domain.Entities;
//
// namespace Application.TaskItmes.Command;
//
// public class DeleteTaskItemCommand
// {
//     private readonly ITaskRepository _taskRepository;
//
//     public DeleteTaskItemCommand(ITaskRepository  taskRepository)
//     {
//         _taskRepository = taskRepository;
//     }
//
//     public async Task<TaskItemResponse> DeleteTaskItem(Guid id)
//     {
//        var Result = await _taskRepository.DeleteAsync(id);
//        if (Result is null)
//        {
//            return new TaskItemResponse(){ IsThereError = true};
//        }
//        var Response = new TaskItemResponse()
//        {
//            IsThereError = false,
//            Name = Result.Name,
//            Description = Result.Description,
//            Created = Result.Created,
//            Status = Result.Status,
//            
//        };
//          return Response;
//          
//     }
// }