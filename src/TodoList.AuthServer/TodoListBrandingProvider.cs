using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace TodoList;

[Dependency(ReplaceServices = true)]
public class TodoListBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "TodoList";
}
