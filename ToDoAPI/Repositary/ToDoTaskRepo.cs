using DataLayer.Entities;
using DataLayer.Entities.EnumClasses;
using DTLayer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositary
{
    public class ToDoTaskRepo :
     Repo<TodoTask, int>
    {

        public ToDoTaskRepo(AppDbContext context)
            : base(context) { }
        public  async Task<bool> UpdateTaskWithPendingStatusAsync(TodoTask entity)
        {
            var rows = await _dbSet.Where(x=>(x.TaskID==entity.TaskID&&x.userID==entity.userID&&x.status==enTaskStatus.pending))
                .ExecuteUpdateAsync(i=>i
                .SetProperty(x=>x.updatedAt,DateTime.UtcNow)
                .SetProperty(x => x.title, entity.title)
.SetProperty(x => x.description,
    x => string.IsNullOrEmpty(entity.description) ? x.description : entity.description)
);
            return rows > 0;
        
        }

        public async Task<bool> UpdateTaskWithProgressStatusAsync(TodoTask entity)
        {
            var rows = await _dbSet.Where(x => (x.TaskID == entity.TaskID && x.userID == entity.userID && x.status == enTaskStatus.progress))
                .ExecuteUpdateAsync(i => i
                .SetProperty(x => x.updatedAt, DateTime.UtcNow)
.SetProperty(x => x.description,
    x => string.IsNullOrEmpty(entity.description) ? x.description : entity.description)
);
            return rows > 0;
        }


        private async Task<bool> _ChangeTaskStatusAsync(enTaskStatus conditionValue, enTaskStatus updateValue, string userID,int taskID)
        {
            var rows = await _dbSet.Where(x => (x.TaskID == taskID&& x.userID == userID && x.status == conditionValue))
                .ExecuteUpdateAsync(i => i
                .SetProperty(x => x.updatedAt, DateTime.UtcNow)
.SetProperty(x => x.status,updateValue)
);
            return rows > 0;
        }

        public async Task<bool> UpdateStatusIntoProgressTaskAsync(string userID, int taskID)
        {
            return await _ChangeTaskStatusAsync(enTaskStatus.pending, enTaskStatus.progress, userID, taskID);
        }

        public async Task<bool> UpdateStatusIntoCanceledTaskAsync(string userID, int taskID)
        {
            var rows = await _dbSet.Where(x => (x.TaskID == taskID && x.userID == userID && (x.status==enTaskStatus.pending||x.status==enTaskStatus.progress)))
        .ExecuteUpdateAsync(i => i
        .SetProperty(x => x.updatedAt, DateTime.UtcNow)
.SetProperty(x => x.status, enTaskStatus.canceld)
);
            return rows > 0;
        }

        public async Task<bool> UpdateStatusIntoDoneTaskAsync(string userID, int taskID)
        {
            return await _ChangeTaskStatusAsync(enTaskStatus.progress, enTaskStatus.Done, userID, taskID);
        }


        public async Task<bool> DeleteTaskAsync(int taskID)
        {
            var rows = await _dbSet.Where(x => x.TaskID == taskID)
        .ExecuteUpdateAsync(i => i
        .SetProperty(x => x.updatedAt, DateTime.UtcNow)
.SetProperty(x => x.status, enTaskStatus.canceld)
.SetProperty(x => x.IsDeleted,true)
);
            return rows > 0;
        }



        // Get All Entities
        public override async Task<List<TodoTask>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        // Get Entity by Key
        public async Task<TodoTask?> GetTaskByIdAndUserIDAsync(int id,string userID)
        {
return await _dbSet.AsNoTracking().SingleOrDefaultAsync(x=>x.TaskID== id&&x.userID==userID);
        }

        public async Task<TodoTask?> GetTaskByIdAndUserIDAsync(int id)
        {
            return await _dbSet.AsNoTracking().SingleOrDefaultAsync(x => x.TaskID == id);
        }


        public async Task<List<TodoTask>> GetUserTasksWithPaginationAsync(int pageNumber, int pageSize,byte value)
        {
            return await _dbSet.AsNoTracking()
                .Where(x=>x.status== (enTaskStatus)value)
                .OrderByDescending(x => x.createdAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }



        //
    }
}
