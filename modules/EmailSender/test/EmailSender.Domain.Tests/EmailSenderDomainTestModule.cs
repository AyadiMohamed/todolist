using Volo.Abp.Modularity;

namespace EmailSender;

[DependsOn(
    typeof(EmailSenderDomainModule),
    typeof(EmailSenderTestBaseModule)
)]
public class EmailSenderDomainTestModule : AbpModule
{

}
