using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EmailSender;

[DependsOn(
    typeof(EmailSenderApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class EmailSenderHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(EmailSenderApplicationContractsModule).Assembly,
            EmailSenderRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<EmailSenderHttpApiClientModule>();
        });

    }
}
