using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using TodoList.Entities.Members;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Uow;
using AbpTenant = Volo.Abp.TenantManagement.Tenant;

namespace TodoList.Data
{
    public class UserDataSeedContributor : ITransientDependency
    {
        //data seeder order
        public int Order => 3;

        private readonly IConfiguration _configuration;
        private readonly IRepository<AbpTenant> _tenantRepository;
        private IdentityUserManager _userManager;
        private IdentityRoleManager _roleManager;
        private IRepository<Member> _memberRepository;
        public UserDataSeedContributor(IConfiguration configuration,
                                           IRepository<AbpTenant> tenantRepository,
                                           IdentityUserManager userManager,
                                           IdentityRoleManager roleManager,
                                           IRepository<Member> memberRepository)
        {
            _configuration = configuration;
            _tenantRepository = tenantRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _memberRepository = memberRepository;
        }
        [UnitOfWork]
        public async Task SeedAsync(DataSeedContext context)
        {

            await CreateUserAsync();

        }

        private async Task CreateUserAsync()
        {


            var roleExist = await _roleManager.RoleExistsAsync("admin");
            if (!roleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole(SimpleGuidGenerator.Instance.Create(), "admin"));
            }
            // default teant user
            var defaultUserEmail = _configuration.GetSection("Users:DefaultUser")["Email"];
            var userExist = await _userManager.FindByEmailAsync(defaultUserEmail);
            if (!defaultUserEmail.IsNullOrWhiteSpace() && userExist == null)
            {
                var defaultUserPassword = _configuration.GetSection("Users:DefaultUser")["Password"];
                var defaultUser = new IdentityUser(SimpleGuidGenerator.Instance.Create(), defaultUserEmail, defaultUserEmail);
                await _userManager.CreateAsync(defaultUser, defaultUserPassword);
                await _userManager.AddToRoleAsync(defaultUser, "admin");
                // default user member
                var defaultUserName = _configuration.GetSection("Users:DefaultUser")["Name"];
                var defaultUserMember = new Member(defaultUserName, defaultUserEmail, defaultUser.Id);
                await _memberRepository.InsertAsync(defaultUserMember);
            }
            // Second  user
            var secondUserEmail = _configuration.GetSection("SecondUser")["Email"];
            var secondUserExist = await _userManager.FindByEmailAsync(secondUserEmail);
            if (!secondUserEmail.IsNullOrWhiteSpace() && secondUserExist == null)
            {
                var secondUserPassword = _configuration.GetSection("Users:SecondUser")["Password"];
                var secondUser = new IdentityUser(SimpleGuidGenerator.Instance.Create(), secondUserEmail, secondUserEmail);
                await _userManager.CreateAsync(secondUser, secondUserPassword);
                await _userManager.AddToRoleAsync(secondUser, "admin");
                // Second user member
                var secondUserName = _configuration.GetSection("Users:SecondUser")["Name"];
                var secondUserMember = new Member(secondUserName, secondUserEmail, secondUser.Id);
                await _memberRepository.InsertAsync(secondUserMember);
            }
        }
    }

}
    

