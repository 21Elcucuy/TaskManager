using Application.DTO;
using Application.Interface;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Identity;
using Infrastructure.Persistence.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class SQLTaskRepository : ITaskRepository
{
     private readonly TaskManagerDbContext _dbContext;
     private readonly UserManager<ApplicationUser> _userManager;

     public SQLTaskRepository(TaskManagerDbContext  dbContext ,UserManager<ApplicationUser> userManager)
     {
          _dbContext = dbContext;
          _userManager = userManager;
     }

     public async Task<TaskItem> CreateAsync(TaskItem item)
     {
          var  User = _userManager.FindByNameAsync(item.UserId).GetAwaiter().GetResult();
 
          if (await _dbContext.TaskItems.FirstOrDefaultAsync(x => ((x.Name == item.Name)&& x.UserId ==item.UserId)) is not null)
          {
               return null;
          }
          
          _dbContext.TaskItems.Add(item);
          await _dbContext.SaveChangesAsync();
          return item;

     }

  

     public async Task<TaskItem> UpdateAsync(TaskItem item)
     {
          var TheItem = await _dbContext.TaskItems.FirstOrDefaultAsync(x => ((x.Id == item.Id)&& x.UserId ==item.UserId));
          if (TheItem is null)
          {
               return null;
          }
             
             TheItem.Name = item.Name;
             TheItem.Description = item.Description;
             TheItem.Status = item.Status;
             await _dbContext.SaveChangesAsync();

             return TheItem;
             // return new TaskItemResponse()
             // {
             //      IsThereError = false, 
             //      Name = TheItem.Name,
             //      Description = TheItem.Description, 
             //      Status = TheItem.Status,
             //      Created = TheItem.Created,
             //      
             // };

     }

     public async Task<TaskItem> DeleteAsync(Guid id , string UserId)
     {
          var TheItem = await _dbContext.TaskItems.FirstOrDefaultAsync(x => ((x.Id == id) && x.UserId ==UserId));
          if (TheItem is null)
          {
               return null;
          }
          _dbContext.TaskItems.Remove(TheItem);
          await _dbContext.SaveChangesAsync();
           return TheItem;
          // return new TaskItemResponse()
          // {
          //      IsThereError = false,
          //      Name = TheItem.Name,
          //      Description = TheItem.Description, 
          //      Status = TheItem.Status,
          //      Created = TheItem.Created,
          // };
     }

     public async Task<IEnumerable<TaskItem>> GetAllAsync(string UserId)
     { 
          return await _dbContext.TaskItems.Where(x => x.UserId == UserId).ToListAsync();
       
         // var ResponseItemsList = new List<TaskItemResponse>();
         // foreach (var item in AllItem)
         // {
         //      ResponseItemsList.Add(new TaskItemResponse()
         //      {
         //           IsThereError = false,
         //           Name = item.Name,
         //           Description = item.Description,
         //           Status = item.Status,
         //           Created = item.Created,
         //      });
         // }
     }

     public async  Task<TaskItem> GetTaskItemByIdAsync(Guid taskId)
     {
          var TheItem = await _dbContext.TaskItems.FirstOrDefaultAsync(x => (x.Id == taskId));
          if (TheItem is null)
          {
               return null;
          }

          return TheItem;
          // return new TaskItemResponse()
          // {
          //      Name = TheItem.Name,
          //      Description = TheItem.Description,
          //      Status = TheItem.Status,
          //      Created = TheItem.Created,
          //
          // };
     }
}