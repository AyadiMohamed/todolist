using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace TodoList.Dtos.MemberDos
{
    public class MemberListingDto : FullAuditedEntityDto<Guid>
    {
        public string MemberName { get; set; }
        public string MemberEmail { get; set; }
        public Guid UserId { get; set; }
    }
}
