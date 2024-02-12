using Volo.Abp.Modularity;

namespace EmailSender;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class EmailSenderDomainTestBase<TStartupModule> : EmailSenderTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
