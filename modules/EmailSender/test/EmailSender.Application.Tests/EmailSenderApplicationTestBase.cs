using Volo.Abp.Modularity;

namespace EmailSender;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class EmailSenderApplicationTestBase<TStartupModule> : EmailSenderTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
