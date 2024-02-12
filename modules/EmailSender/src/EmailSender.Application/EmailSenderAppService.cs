using EmailSender.Localization;
using Volo.Abp.Application.Services;

namespace EmailSender;

public abstract class EmailSenderAppService : ApplicationService
{
    protected EmailSenderAppService()
    {
        LocalizationResource = typeof(EmailSenderResource);
        ObjectMapperContext = typeof(EmailSenderApplicationModule);
    }
}
