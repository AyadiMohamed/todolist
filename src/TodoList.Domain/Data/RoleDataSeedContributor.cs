using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Uow;

namespace TodoList.Data
{
    public class RoleDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        //data seeder order
        public int Order => 1;

        private IdentityRoleManager _roleManager;
        private readonly IConfiguration _configuration;

        public RoleDataSeedContributor(IdentityRoleManager roleManager, IConfiguration configuration)
        {
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [UnitOfWork]
        public async Task SeedAsync(DataSeedContext context)
        {
            await this.CreateRolesAsync(context);
        }

        private async Task CreateRolesAsync(DataSeedContext context)
        {
            // admine role 
            var adminRoleExist = await _roleManager.RoleExistsAsync("admin");
            if (!adminRoleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole(SimpleGuidGenerator.Instance.Create(), "admin", context.TenantId));
            }
            // member role 
            var roleName = _configuration.GetSection("Roles:Member")["RoleName"];
            if (!roleName.IsNullOrWhiteSpace())
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(SimpleGuidGenerator.Instance.Create(), roleName, context.TenantId));
                }
            }

        }
    }
}
