using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EmailSender.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
