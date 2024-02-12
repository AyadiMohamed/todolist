using Volo.Abp.Modularity;

namespace EmailSender;

[DependsOn(
    typeof(EmailSenderApplicationModule),
    typeof(EmailSenderDomainTestModule)
    )]
public class EmailSenderApplicationTestModule : AbpModule
{

}
