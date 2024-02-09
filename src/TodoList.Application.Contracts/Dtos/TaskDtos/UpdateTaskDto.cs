using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace TodoList.Dtos.TaskDtos
{
    public class UpdateTaskDto : AuditedEntityDto<Guid>
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public bool Completed { get; set; }


        private Guid UserId { get; set; }
        public Guid? MemberId { get; set; }


        public void SetUserId(Guid userId)
        {
            this.UserId = userId;
        }

        public Guid GetUserId()
        {
            return this.UserId;
        }

    }
}
