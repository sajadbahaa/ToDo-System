using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dtos
{
    public  class updatePendingTaskDto
    {
        public int taskID {  get; set; }
        
        [JsonIgnore]
        public string userID { get; set; } = string.Empty;
        public string title { get; set; } = null!;
        public string? description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
