using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace TodoList.Data
{
    public class PermissionDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private IdentityRoleManager _roleManager;
        private readonly IConfiguration _configuration;

        protected IPermissionDefinitionManager _permissionDefinitionManager;
        protected IPermissionDataSeeder _permissionDataSeeder;



        public PermissionDataSeedContributor(
            IdentityRoleManager roleManager,
            IConfiguration configuration,
            IPermissionDefinitionManager permissionDefinitionManager,
            IPermissionDataSeeder permissionDataSeeder
           )
        {
            _roleManager = roleManager;
            _configuration = configuration;
            _permissionDefinitionManager = permissionDefinitionManager;
            _permissionDataSeeder = permissionDataSeeder;

        }

        public virtual async Task SeedAsync(DataSeedContext context)
        {
            await this.SeedMembersPermissionsAsync(context);
        }

        public async Task SeedMembersPermissionsAsync(DataSeedContext context)
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
                    "TodoListTasks.Create",
                    "TodoListTasks.Update",
                    "TodoListTasks.Delete",
                    "TodoListTasks.Read",

                };

                await _permissionDataSeeder.SeedAsync(RolePermissionValueProvider.ProviderName,
                     roleName, permissionNames);

            }
        }
    }
}
