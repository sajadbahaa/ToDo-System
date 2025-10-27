using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dtos
{
    public  class addTaskDtos
    {
        [JsonIgnore]
        public string userID {  get; set; } 
        public string title { get; set; } = null!;
        public string? description { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime DueDate { get; set; }
    public addTaskDtos()
        {
            createdAt = DateTime.UtcNow;
            DueDate = DateTime.UtcNow;
            title = string.Empty;
        userID= string.Empty;
        }
    }
}
