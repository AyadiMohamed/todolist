using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace TodoList.Dtos.TaskDtos
{
    public class UpdateTaskDto : EntityDto<Guid>
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public bool Completed { get; set; } = false;
        public Guid? AssignedTo { get; set; }

        public string? MemberName { get; set; }

        private Guid MemberId { get; set; }


        public void SetMemberId(Guid memberId)
        {
            this.MemberId = memberId;
        }

        public Guid GetMemberId()
        {
            return this.MemberId;
        }

    }
}
