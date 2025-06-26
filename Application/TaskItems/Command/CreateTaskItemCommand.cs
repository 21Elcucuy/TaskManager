// using Application.DTO;
// using Application.Interface;
// using Domain.Entities;
// using Domain.Enums;
//
// namespace Application.TaskItmes.Command;
//
// public class CreateTaskItemCommand
// {
//     private readonly ITaskRepository _taskRepository;
//
//     public CreateTaskItemCommand(ITaskRepository  taskRepository)
//     {
//         _taskRepository = taskRepository;
//     }
//
//     public async Task<TaskItemResponse> CreateTaskItem(AddTaskItem AddtaskItem)
//     {
//         var NewItem = new TaskItem()
//         {
//             Name = AddtaskItem.Name,
//             Description = AddtaskItem.Description,
//             Created = DateTime.Now,
//             Status = Status.Done,
//         };
//        var response =  await _taskRepository.CreateAsync(NewItem);
//        if (response is null)
//        {
//            return new TaskItemResponse()
//            {
//                IsThereError = true
//            };
//        }
//        var ResponseItem = new TaskItemResponse()
//         {
//             IsThereError = false,
//             Name = AddtaskItem.Name,
//             Description = AddtaskItem.Description,
//             Created = DateTime.Now,
//             Status = Status.Done,
//         };
//         return ResponseItem;
//     }
//     
// }