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
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public task(string title , string description , DateTime dueDate , bool completed , Guid userId, string userName, string userEmail) {
            this.Title = title;
            this.Description = description;
            this.DueDate = dueDate;
            this.Completed = completed;
            this.UserId = userId;
            this.UserName = userName;
            this.UserEmail = userEmail;
        } 
    }

}
