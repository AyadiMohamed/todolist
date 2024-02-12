using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EmailSender;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class EmailSenderInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<EmailSenderInstallerModule>();
        });
    }
}
