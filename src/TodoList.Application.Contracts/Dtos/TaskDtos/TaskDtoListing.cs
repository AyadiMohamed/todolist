using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace TodoList.Dtos.TaskDtos
{
    public class TaskDtoListing : EntityDto<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool Completed { get; set; }

        public Guid MemberId { get; set; }
        public string? MemberName { get; set; }

    }
}
