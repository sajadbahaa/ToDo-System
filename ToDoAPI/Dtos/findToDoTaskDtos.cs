using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public  class findToDoTaskDtos
    {
        public int TaskID { get; init; }
        public string userID { get; init; } = null!;
        public string title { get; init; } = null!;
        public string? description { get; init; }
        public bool IsDeleted { get; init; }
        public string status { get; init; }
        public DateTime createdAt { get; init; }
        public DateTime? updatedAt { get; init; }
        public DateTime DueDate { get; init; }
    }
}
