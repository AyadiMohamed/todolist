using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                    "TodoList.Tasks.Default",
                    "TodoList.Tasks.Create",
                    "TodoList.Tasks.Update",
                    "TodoList.Tasks.Delete",
                    "TodoList.Tasks.Read",
                    "TodoList.Members.Default",
                    "TodoList.Members.Create",
                    "TodoList.Members.Update",
                    "TodoList.Members.Delete",
                    "TodoList.Members.Read"
                };

                await _permissionDataSeeder.SeedAsync(RolePermissionValueProvider.ProviderName, roleName, permissionNames);
            }
        }
    }
}
