using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace TodoList.Entities
{
    public class User : FullAuditedAggregateRoot<Guid>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        // Other user properties as needed
        public ICollection<Task> Tasks { get; set; }
    }
}
