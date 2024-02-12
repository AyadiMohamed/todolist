using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace EmailSender;

[DependsOn(
    typeof(EmailSenderDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class EmailSenderApplicationContractsModule : AbpModule
{

}
