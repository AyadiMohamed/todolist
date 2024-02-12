using EmailSender.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EmailSender;

public abstract class EmailSenderController : AbpControllerBase
{
    protected EmailSenderController()
    {
        LocalizationResource = typeof(EmailSenderResource);
    }
}
