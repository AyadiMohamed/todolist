using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace TodoList.Dtos.TaskDtos
{
    public class TaskDto : AuditedEntityDto<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool Completed { get; set; }

        public Guid MemberId { get; set; }
        public string? MemberName { get; set; }

        public TaskDto() { }

    }
}
