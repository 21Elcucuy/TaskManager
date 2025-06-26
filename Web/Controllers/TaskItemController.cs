using System.Security.Claims;
using Application.DTO;
using Application.Interface;
using Application.TaskItems;

using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.TaskManagerControllers;
[ApiController]
[Route("api/[controller]")]
public class TaskItemController : ControllerBase
{
   private readonly ITaskItemServices _taskitem;


   public TaskItemController(ITaskItemServices taskitem)
   {
      _taskitem = taskitem;
      
   }
   [HttpGet]
   public async Task<IActionResult> GetAllTaskItem()
   {
      var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      var Response = await   _taskitem.GetAll(UserId);
      return Ok(Response);
   }

   [HttpPost]
   public async Task<IActionResult> AddTaskItem([FromBody] AddTaskItem addtaskItem)
   {
      var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      var Response =    await   _taskitem.CreateTaskItem(addtaskItem,UserId);
      if (Response.IsThereError == true)
      {
         return BadRequest();
      }
      return Ok(Response);
      
   }

   [HttpPut]
   public async Task<IActionResult> UpdateTaskItem([FromBody]UpdateTaskItem updatetaskItem)
   {
      var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      var Response = await    _taskitem.UpdateTaskItem(updatetaskItem,UserId);
      if (Response.IsThereError == true)
      {
         return BadRequest();
         
      }
      return Ok(Response);
   }

   [HttpDelete("{Id}")]
   public async Task<IActionResult> DeleteTaskItem([FromQuery]Guid Id)
   {
      var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      var Response = await   _taskitem.DeleteTaskItem(Id,UserId);
      if (Response.IsThereError == true)
         return BadRequest();
      return Ok(Response);
   }

   [HttpGet("{id}")]
   public async Task<IActionResult> GetTaskItemById([FromQuery]Guid id)
   { 
      var Response = await   _taskitem.GetTaskItemById(id);
       if (Response.IsThereError == true)
          return BadRequest();
       return Ok(Response);
   }
   
}