// using Application.DTO;
// using Application.Interface;
// using Domain.Entities;
//
// namespace Application.TaskItmes.Command;
//
// public class UpdateTaskItemCommand
// {
//     private readonly ITaskRepository _taskRepository;
//
//     public UpdateTaskItemCommand(ITaskRepository  taskRepository)
//     {
//         _taskRepository = taskRepository;
//     }
//
//     public async Task<TaskItemResponse> UpdateTaskItem(UpdateTaskItem updateTaskItem)
//     {
//         var UpdatedItem = new TaskItem()
//         {
//             Id = updateTaskItem.Id,
//             Name = updateTaskItem.Name,
//             Description = updateTaskItem.Description,
//             Status = updateTaskItem.Status,
//         };
//         var Result =  await _taskRepository.UpdateAsync(UpdatedItem);
//         if (Result is null)
//         {
//             return new TaskItemResponse()
//             {
//                 IsThereError = true
//             };
//         }
//
//         var respnse = new TaskItemResponse()
//         {
//             IsThereError = false,
//             Name = Result.Name,
//             Description = Result.Description,
//             Status = Result.Status,
//             Created = Result.Created,
//         };
//         return respnse;
//     }
// }