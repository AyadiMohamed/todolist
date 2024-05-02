using System;
using TodoList.Entities.Members;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace TodoList.Models.Members
{
    public class task : Entity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool Completed { get; set; } = false;
        public Guid? AssignedTo { get; set; }

        public Guid? MemberId { get; set; }
        public Member? Member { get; set; }

        public task(string title, string description, DateTime dueDate, bool completed , Guid memberId)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            Completed = completed;
            MemberId = memberId;

        }
    }

}
