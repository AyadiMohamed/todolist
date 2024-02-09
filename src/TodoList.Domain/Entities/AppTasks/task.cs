using System;
using System.Collections.Generic;
using System.Linq;

using Volo.Abp.Domain.Entities.Auditing;

namespace TodoList.Entities.Tasks
{
    public class task : FullAuditedAggregateRoot<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool Completed { get; set; } = false;
        public Guid UserId { get; set; }
        public Guid MemberId { get; set; }

        public task(string title , string description , DateTime dueDate , bool completed , Guid userId, Guid memberId) {
            this.Title = title;
            this.Description = description;
            this.DueDate = dueDate;
            this.Completed = completed;
            this.UserId = userId;
            this.MemberId = memberId;
        } 
    }

}
