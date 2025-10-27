using AutoMapper;

using DataLayer.Entities;
using Dtos;
using Repositary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer
{
    public class ToDoTaskServices
    {
        readonly ToDoTaskRepo _repo;
        readonly IMapper _mapper;

       

        public ToDoTaskServices(ToDoTaskRepo repo,IMapper mapper)
        {
            _repo = repo; 
            _mapper = mapper;
        }

        // find by id
        public async Task<findToDoTaskDtos> FindToDoTaskAsync(int taskID,string userID , bool isAdmin =false)
        {
            if (isAdmin)
                return _mapper.Map<findToDoTaskDtos>(await _repo.GetTaskByIdAndUserIDAsync(taskID));

            return _mapper.Map<findToDoTaskDtos>(await _repo.GetTaskByIdAndUserIDAsync(taskID,userID));
        }

        // find all 
        public async Task<List<findToDoTaskDtos>> FindAllToDoTaskAsync()
        {
            return _mapper.Map<List<findToDoTaskDtos>>(await _repo.GetAllAsync());
        }

        //GetUserTasksWithPaginationAsync
        // insert task .

        public async Task<List<findToDoTaskDtos>> FindToDoTaskWithPaganationAsync(int pageNumber,int PageCount,byte value)
        {
            return _mapper.Map<List<findToDoTaskDtos>>(await _repo.GetUserTasksWithPaginationAsync(pageNumber,PageCount,value));
        }

        public async Task<bool> addTaskAsync(addTaskDtos dto,string userID)
        {
            dto.userID = userID;
            var res = _mapper.Map<TodoTask>(dto);
            return await _repo.AddAsync(res);
        }


        // updte task when status == pending.

        public async Task<bool> updatePendingTaskAsync(updatePendingTaskDto dto, string userID)
        {
            dto.userID = userID;
            var res = _mapper.Map<TodoTask>(dto);
            return await _repo.UpdateTaskWithPendingStatusAsync(res);
        }

        // updte task when status == progress.
        public async Task<bool> updateProgressTaskAsync(updateProgressTaskDto dto, string userID)
        {
            dto.userID = userID;
            var res = _mapper.Map<TodoTask>(dto);
            return await _repo.UpdateTaskWithProgressStatusAsync(res);
        }


        // update task into done status.
        public async Task<bool> UpdateStatusIntoDoneTaskAsync(int taskID, string userID)
        {
       
            return await _repo.UpdateStatusIntoDoneTaskAsync(userID, taskID);
        }


        // cancel task by user
        public async Task<bool> UpdateStatusIntoCanceledTaskAsync(int taskID, string userID)
        {

            return await _repo.UpdateStatusIntoCanceledTaskAsync(userID, taskID);
        }


        // update task into progress status.
        public async Task<bool> UpdateStatusIntoProgressTaskAsync(int taskID, string userID)
        {
            return await _repo.UpdateStatusIntoProgressTaskAsync(userID, taskID);
        }


        // Delete Task Soft by admin..
        public async Task<bool> DeleteTaskAsync(int taskID)
        {
            return await _repo.DeleteTaskAsync(taskID);
        }




    }
}
