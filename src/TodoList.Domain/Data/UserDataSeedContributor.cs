using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using TodoList.Entities.Members;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Uow;

namespace TodoList.Data
{
    public class UserDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public int Order => 2;

        private readonly IConfiguration _configuration;
        private readonly IdentityUserManager _userManager;
        private readonly IdentityRoleManager _roleManager;
        private readonly IRepository<Member> _memberRepository;

        public UserDataSeedContributor(
            IConfiguration configuration,
            IdentityUserManager userManager,
            IdentityRoleManager roleManager,
            IRepository<Member> memberRepository)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _memberRepository = memberRepository;
        }

        [UnitOfWork]
        public async Task SeedAsync(DataSeedContext context)
        {
            await SeedUserAndRoleAsync();
        }

        private async Task SeedUserAndRoleAsync()
        {
            var adminRoleName = _configuration["Users:Roles:Admin:RoleName"];
            var adminRoleExist = await _roleManager.FindByNameAsync(adminRoleName);
            if (adminRoleExist == null)
            {
                var adminRole = new IdentityRole(SimpleGuidGenerator.Instance.Create(), adminRoleName);
                await _roleManager.CreateAsync(adminRole);
            }

            var defaultUserEmail = _configuration["Users:DefaultUser:Email"];
            if (!string.IsNullOrWhiteSpace(defaultUserEmail))
            {
                var userExist = await _userManager.FindByEmailAsync(defaultUserEmail);
                if (userExist == null)
                {
                    var defaultUserPassword = _configuration["Users:DefaultUser:Password"];
                    var defaultUserName = _configuration["Users:DefaultUser:Name"];

                    var defaultUser = new IdentityUser(SimpleGuidGenerator.Instance.Create(), defaultUserEmail, defaultUserEmail);
                    await _userManager.CreateAsync(defaultUser, defaultUserPassword);
                    await _userManager.AddToRoleAsync(defaultUser, adminRoleName);

                    var defaultUserMember = new Member(defaultUserName, defaultUserEmail, defaultUser.Id);
                    await _memberRepository.InsertAsync(defaultUserMember);
                }
            }

            var secondUserEmail = _configuration["Users:SecondUser:Email"];
            if (!string.IsNullOrWhiteSpace(secondUserEmail))
            {
                var secondUserExist = await _userManager.FindByEmailAsync(secondUserEmail);
                if (secondUserExist == null)
                {
                    var secondUserPassword = _configuration["Users:SecondUser:Password"];
                    var secondUserName = _configuration["Users:SecondUser:Name"];

                    var secondUser = new IdentityUser(SimpleGuidGenerator.Instance.Create(), secondUserEmail, secondUserEmail);
                    await _userManager.CreateAsync(secondUser, secondUserPassword);
                    await _userManager.AddToRoleAsync(secondUser, adminRoleName);

                    var secondUserMember = new Member(secondUserName, secondUserEmail, secondUser.Id);
                    await _memberRepository.InsertAsync(secondUserMember);
                }
            }
        }
    }
}
