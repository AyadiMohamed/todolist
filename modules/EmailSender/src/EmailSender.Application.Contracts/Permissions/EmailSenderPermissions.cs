using Volo.Abp.Reflection;

namespace EmailSender.Permissions;

public class EmailSenderPermissions
{
    public const string GroupName = "EmailSender";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(EmailSenderPermissions));
    }
}
