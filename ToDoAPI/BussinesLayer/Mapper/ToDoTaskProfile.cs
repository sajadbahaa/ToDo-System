using AutoMapper;
using DataLayer.Entities;
using Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Mapper
{
    public  class ToDoTaskProfile:Profile
    {
        public ToDoTaskProfile()
        {
            CreateMap<TodoTask, findToDoTaskDtos>()
                .ForMember(x=>x.TaskID,opt=>opt.MapFrom(y=>y.TaskID))
                .ForMember(x => x.status, opt => opt.MapFrom(y => y.status.ToString()));



            CreateMap<addTaskDtos, TodoTask>()
                .ForMember(x => x.TaskID, opt => opt.Ignore())
                .ForMember(x => x.user, opt => opt.Ignore());

            CreateMap<updateProgressTaskDto, TodoTask>()
                .ForMember(x => x.TaskID, opt => opt.MapFrom(y => y.taskID));

            CreateMap<updatePendingTaskDto, TodoTask>()
                .ForMember(x => x.TaskID, opt => opt.MapFrom(y => y.taskID));




        }
    }
}
