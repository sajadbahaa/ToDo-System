using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dtos
{
    public  class updateProgressTaskDto
    {
        public int taskID { get; set; }
        
        [JsonIgnore]
        public string userID { get; set; }  = string.Empty;
        public string? description { get; set; }

    }
}
