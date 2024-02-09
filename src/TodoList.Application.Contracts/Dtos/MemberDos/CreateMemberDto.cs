using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace TodoList.Dtos.MemberDos
{
    public class CreateMemberDto : FullAuditedEntityDto<Guid>
    {
        [Required]
        public string MemberName { get; set; }
        [Required]
        public string MemberEmail { get; set; }
        private Guid UserId { get; set; }

        public CreateMemberDto()
        {

        }

        public void SetUserID(Guid userId)
        {
            this.UserId = userId;
        }

        public Guid GetUserId()
        {
            return this.UserId;
        }

    }
   
}
