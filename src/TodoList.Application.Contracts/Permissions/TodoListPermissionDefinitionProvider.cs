using TodoList.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace TodoList.Permissions;

public class TodoListPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TodoListPermissions.GroupName);

        //task permissions
        var taskPermission = myGroup.AddPermission(TodoListPermissions.TodoListTasks.Default, L("Permission:Tasks"));
        taskPermission.AddChild(TodoListPermissions.TodoListTasks.Create, L("Permission:Tasks.Create"));
        taskPermission.AddChild(TodoListPermissions.TodoListTasks.Update, L("Permission:Tasks.Update"));
        taskPermission.AddChild(TodoListPermissions.TodoListTasks.Delete, L("Permission:Tasks.Delete"));
        taskPermission.AddChild(TodoListPermissions.TodoListTasks.Read, L("Permission:Tasks.Read"));

        //member permission
        var memberPermission = myGroup.AddPermission(TodoListPermissions.TodoListMembers.Default, L("Permission:Tasks"));
        taskPermission.AddChild(TodoListPermissions.TodoListMembers.Create, L("Permission:Tasks.Create"));
        taskPermission.AddChild(TodoListPermissions.TodoListMembers.Update, L("Permission:Tasks.Update"));
        taskPermission.AddChild(TodoListPermissions.TodoListMembers.Delete, L("Permission:Tasks.Delete"));
        taskPermission.AddChild(TodoListPermissions.TodoListMembers.Read, L("Permission:Tasks.Read"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TodoListResource>(name);
    }
}
