using Localization.Resources.AbpUi;
using EmailSender.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EmailSender;

[DependsOn(
    typeof(EmailSenderApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class EmailSenderHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(EmailSenderHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<EmailSenderResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
