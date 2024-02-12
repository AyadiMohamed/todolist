using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;

namespace TodoList.Data
{
    public class TodoListPermissionDataSeedContributor : IDataSeedContributor, ITransientDependency
    {

        private readonly IdentityRoleManager _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IPermissionDefinitionManager _permissionDefinitionManager;
        private readonly IPermissionDataSeeder _permissionDataSeeder;

        public TodoListPermissionDataSeedContributor(
            IdentityRoleManager roleManager,
            IConfiguration configuration,
            IPermissionDefinitionManager permissionDefinitionManager,
            IPermissionDataSeeder permissionDataSeeder)
        {
            _roleManager = roleManager;
            _configuration = configuration;
            _permissionDefinitionManager = permissionDefinitionManager;
            _permissionDataSeeder = permissionDataSeeder;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await SeedTodoListPermissionsAsync(context);
        }

        private async Task SeedTodoListPermissionsAsync(DataSeedContext context)
        {
            var roleName = _configuration.GetSection("Roles:Member")["RoleName"];
            if (!roleName.IsNullOrWhiteSpace())
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(SimpleGuidGenerator.Instance.Create(), roleName));
                }

                var permissionNames = new List<string>
                {
                    // Add your TodoList permission names here
                    TodoListPermissions.TodoListTasks.Default,
                    TodoListPermissions.TodoListTasks.Create,
                    TodoListPermissions.TodoListTasks.Update,
                    TodoListPermissions.TodoListTasks.Delete,
                    TodoListPermissions.TodoListTasks.Read,
                    TodoListPermissions.TodoListMembers.Default,
                    TodoListPermissions.TodoListMembers.Create,
                    TodoListPermissions.TodoListMembers.Update,
                    TodoListPermissions.TodoListMembers.Delete,
                    TodoListPermissions.TodoListMembers.Read
                };

                await _permissionDataSeeder.SeedAsync(RolePermissionValueProvider.ProviderName, roleName, permissionNames);
            }
        }
    }
}
