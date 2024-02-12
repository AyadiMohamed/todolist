namespace EmailSender;

public static class EmailSenderDbProperties
{
    public static string DbTablePrefix { get; set; } = "EmailSender";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "EmailSender";
}
