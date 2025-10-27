using DataLayer.Entities.EnumClasses;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public  class TodoTask
    {
        public int TaskID { get; set; }
        public string userID { get; set; } = null!;
        public string title {  get; set; } = null!;
        public string ?description { get; set; }
        public bool IsDeleted {  get; set; }
        public enTaskStatus status { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public AppUser user { get; set; }

        public TodoTask()
        {

            IsDeleted = false;
            status = enTaskStatus.pending;
            createdAt = DateTime.UtcNow;
            DueDate = DateTime.UtcNow;
        }
    }
}
