using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EmailSender;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EmailSenderHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class EmailSenderConsoleApiClientModule : AbpModule
{

}
