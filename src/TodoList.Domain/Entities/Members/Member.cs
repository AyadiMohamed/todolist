using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Entities.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace TodoList.Entities.Members
{
    public class Member : FullAuditedAggregateRoot<Guid>
    {
        public string MemberName { get; set; }
        public string MemberEmail { get; set; }
        public Guid UserId { get; set; }
        public ICollection<task> Task { get; set; }

        public Member(string memberName, string memberEmail, Guid userId)
        {
            MemberName = memberName;
            MemberEmail = memberEmail;
            UserId = userId;
  
        }
    }
}
