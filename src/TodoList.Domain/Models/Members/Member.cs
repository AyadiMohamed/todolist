using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models.Members;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace TodoList.Entities.Members
{
    public class Member : FullAuditedAggregateRoot<Guid>
    {
   
        public string MemberName { get; set; }
        public string MemberEmail { get; set; }
        public Guid? UserId { get; set; }
        public ICollection<task>? Tasks { get; private set; }

        private Member()
        {

        }

        internal Member(
            Guid id ,
            string memberName,
            string memberEmail, 
            Guid? userId) : base(id)
        {
            Id = id;
            MemberName = Check.NotNullOrEmpty(memberName, nameof(memberName));
            MemberEmail = memberEmail;
            if(userId != null )
            {
                UserId = userId;
            }
            Tasks = new Collection<task>(); 
        }

    }
}
