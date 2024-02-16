using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace TodoList.Dtos.MemberDos
{
    public class UpdateMemberDto : EntityDto<Guid>
    {
        [Required]
        public string MemberName { get; set; }
        [Required]
        public string MemberEmail { get; set; }
    }
}
