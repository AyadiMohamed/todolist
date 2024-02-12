using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EmailSender.EntityFrameworkCore;

[ConnectionStringName(EmailSenderDbProperties.ConnectionStringName)]
public interface IEmailSenderDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
