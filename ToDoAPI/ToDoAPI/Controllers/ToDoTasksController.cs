using BussinesLayer;

using Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // حماية كل الـ endpoints
    public class ToDoTasksController : ControllerBase
    {
                private readonly ToDoTaskServices _taskServices;

        public ToDoTasksController(ToDoTaskServices taskServices)
        {
            _taskServices = taskServices;
        }

        private string GetUserIdFromToken()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User is not authorized.");
            return userId;
        }

        // GET: api/ToDoTasks/{id}
        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            
            string userId = GetUserIdFromToken();
            bool isAdmin = User.IsInRole("Admin");

            var task = await _taskServices.FindToDoTaskAsync(id, userId,isAdmin);
            if (task == null)
                throw new KeyNotFoundException("Task not found.");

            return Ok(task);
        }

        // GET: api/ToDoTasks
        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskServices.FindAllToDoTaskAsync();
            return Ok(tasks);
        }

        // GET: api/ToDoTasks/GetWithPagination
        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetWithPagination(int pageNumber, int pageCount, byte status = 1)
        {
            var tasks = await _taskServices.FindToDoTaskWithPaganationAsync(pageNumber, pageCount, status);
            return Ok(tasks);
        }

        // POST: api/ToDoTasks
        [HttpPost("[action]")]
        public async Task<IActionResult> AddTask([FromBody] addTaskDtos dto)
        {
            string userId = GetUserIdFromToken();

            var result = await _taskServices.addTaskAsync(dto, userId);
            if (!result)
                throw new Exception("Failed to add task.");

            return Ok("Task added successfully.");
        }

        // PUT: api/ToDoTasks/pending
        [HttpPut("[action]")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> UpdatePending([FromBody] updatePendingTaskDto dto)
        {
            string userId = GetUserIdFromToken();

            var result = await _taskServices.updatePendingTaskAsync(dto, userId);
            if (!result)
                throw new Exception("Failed to update pending task.");

            return Ok("Task updated to pending successfully.");
        }

        // PUT: api/ToDoTasks/progress
        [HttpPut("[action]")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> UpdateProgress([FromBody] updateProgressTaskDto dto)
        {
            string userId = GetUserIdFromToken();

            var result = await _taskServices.updateProgressTaskAsync(dto, userId);
            if (!result)
                throw new Exception("Failed to update progress task.");

            return Ok("Task updated to progress successfully.");
        }

        // PUT: api/ToDoTasks/done/{id}
        [HttpPut("([action]/{id:int}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> UpdateToDone(int id)
        {
            string userId = GetUserIdFromToken();

            var result = await _taskServices.UpdateStatusIntoDoneTaskAsync(id, userId);
            if (!result)
                throw new Exception("Failed to mark task as done.");

            return Ok("Task marked as done successfully.");
        }

        // PUT: api/ToDoTasks/canceled/{id}
        [HttpPut("canceled/{id:int}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> CancelTask(int id)
        {
            string userId = GetUserIdFromToken();

            var result = await _taskServices.UpdateStatusIntoCanceledTaskAsync(id, userId);
            if (!result)
                throw new Exception("Failed to cancel task.");

            return Ok("Task canceled successfully.");
        }

        // PUT: api/ToDoTasks/progress/{id}
        [HttpPut("[action]/{id:int}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> UpdateToProgress(int id)
        {
            string userId = GetUserIdFromToken();

            var result = await _taskServices.UpdateStatusIntoProgressTaskAsync(id, userId);
            if (!result)
                throw new Exception("Failed to update task to progress.");

            return Ok("Task updated to progress successfully.");
        }

        // DELETE: api/ToDoTasks/{id}
        [HttpDelete("[action]/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _taskServices.DeleteTaskAsync(id);
            if (!result)
                throw new Exception("Failed to delete task.");

            return Ok("Task deleted successfully.");
        }



        // ------------------ HTTP Client Factory Endpoint ------------------

     
    }
}
