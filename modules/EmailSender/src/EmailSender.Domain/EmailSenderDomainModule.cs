using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace EmailSender;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(EmailSenderDomainSharedModule)
)]
public class EmailSenderDomainModule : AbpModule
{

}
