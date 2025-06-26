// using Application.DTO;
// using Application.Interface;
//
// namespace Application.TaskItems.Query;
//
// public class GetAllQuery
// {
//     private readonly ITaskRepository _taskRepository;
//
//     public GetAllQuery(ITaskRepository  taskRepository)
//     {
//         _taskRepository = taskRepository;
//     }
//
//     public async Task<List<TaskItemResponse>> GetAll()
//     {
//         var result = await _taskRepository.GetAllAsync();
//         var Response = new List<TaskItemResponse>();
//         foreach (var task in result)
//         {
//             Response.Add(new TaskItemResponse()
//             {
//                  Name = task.Name,
//                  Description = task.Description,
//                  IsThereError =false,
//                  Created = task.Created,
//                  Status = task.Status,
//             }
//             );
//         }
//         return Response;
//     }
// }