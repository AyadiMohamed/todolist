using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace TodoList.Dtos
{
    public class Task : AuditedEntityDto<Guid>
    {
      
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public bool Completed { get; set; }

        // Foreign key for the associated user
        [Required]
        public int UserId { get; set; }
    }
}
