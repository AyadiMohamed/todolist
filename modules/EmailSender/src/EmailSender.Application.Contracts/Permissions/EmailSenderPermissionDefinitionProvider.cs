using EmailSender.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EmailSender.Permissions;

public class EmailSenderPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(EmailSenderPermissions.GroupName, L("Permission:EmailSender"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EmailSenderResource>(name);
    }
}
