using System;
using TodoList.Dtos.MemberDos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using Microsoft.AspNetCore.Authorization;
using TodoList.Permissions;
using System.Threading.Tasks;
using Volo.Abp;
using TodoList.Entities.Members;
using System.Linq;
using System.Collections.Generic;

namespace TodoList.Members
{
    public class MembersAppService : CrudAppService<Member, MemberDto, MemberListingDto, Guid,PagedAndSortedResultRequestDto, CreateMemberDto, UpdateMemberDto>
    {
        private IdentityUserManager _userManager;
        private IdentityRoleManager _roleManager;
        public MembersAppService(IRepository<Member, Guid> repository , IdentityUserManager userManager, IdentityRoleManager roleManager  ) : base(repository)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        #region GetAsync
        [Authorize(TodoListPermissions.TodoListMembers.Read)]
        public override async Task<MemberDto> GetAsync(Guid id)
        {
            var result = await base.GetAsync(id);
            return result;
        }
        #endregion

        #region GetListAsync
        [Authorize(TodoListPermissions.TodoListMembers.Read)]
        public override async Task<PagedResultDto<MemberListingDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var query = await this.Repository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, true);
            var totalCount = await this.Repository.CountAsync();
            var entityDtos = new List<MemberListingDto>();
            if (totalCount > 0)
            {
                entityDtos = await MapToGetListOutputDtosAsync(query);
            }
            return new PagedResultDto<MemberListingDto>(totalCount, entityDtos);
        }
        #endregion

        #region Create
        /// <summary>
        /// BeforeCreateAsync :Handel the verification need before creating new member and create the user 
        /// related to that member returns the Id of the user to be used as user Id in the member
        /// </summary>
        /// <param name="input">CreateMemberDto</param>
        /// <returns>Guid : user Id </returns>
        private async Task<Guid> BeforeCreateAsync(CreateMemberDto input)
        {
            // member existins verfication
            var member = await this.Repository.FirstOrDefaultAsync(m => m.MemberName == input.MemberName);
            if (member != null)
                throw new UserFriendlyException(TodoListDomainErrorCodes.TODOLIST_MEMBER_ALREADY_EXIST);
            //create user
            var user = new IdentityUser(GuidGenerator.Create(), input.MemberName, input.MemberEmail);
            var createUserResult = await _userManager.CreateAsync(user);
            if (!createUserResult.Succeeded)
                throw new UserFriendlyException(TodoListDomainErrorCodes.TODOLIST_MEMBER_ALREADY_EXIST + string.Join(", ", createUserResult.Errors.Select(e => e.Code)));
            //Role
            var roleExist = await _roleManager.RoleExistsAsync(TodoListConsts.MemberRole);
            if (!roleExist)
                await _roleManager.CreateAsync(new IdentityRole(GuidGenerator.Create(), TodoListConsts.MemberRole, this.CurrentTenant.Id));

            await _userManager.AddToRoleAsync(user, TodoListConsts.MemberRole);
            await _userManager.AddPasswordAsync(user, TodoListConsts.MemberPassword);
            return user.Id;

        }
        /// <summary>
        /// CreateAsync :Handel the creation of the member and calls the befor and after function
        /// </summary>
        /// <param name="input" >CreateMemberDto</param>
        /// <returns></returns>
        [Authorize(TodoListPermissions.TodoListMembers.Create)]
        public override async Task<MemberDto> CreateAsync(CreateMemberDto input)
        {
            var userId = await this.BeforeCreateAsync(input);
            input.SetUserID(userId);
            var result = await base.CreateAsync(input);
            return result;
        }
        #endregion

        #region Update
        /// <summary>
        /// BeforeUpdateAsync :Handel the verification need before Updating new member 
        /// </summary>
        /// <param name="input">UpdateMemberDto</param>
        /// <returns>Guid : Member </returns>
        private async Task<Member> BeforeUpdateAsync(UpdateMemberDto input)
        {
            // member existins verfication
            var member = await this.Repository.FirstOrDefaultAsync(m => m.MemberName == input.MemberName);
            if (member == null)
                throw new UserFriendlyException(TodoListDomainErrorCodes.TODOLIST_MEMBER_WITH_ID_NOT_FOUND, input.Id.ToString());
            return member;
        }
        /// <summary>
        /// UpdateAsync :Handel the update of the member and calls the befor and after function
        /// </summary>
        /// <param name="id" >Guid:member Id</param>
        /// <param name="input" >UpdateMemberDto</param>
        /// <returns>MemberDto</returns>
        [Authorize(TodoListPermissions.TodoListMembers.Update)]
        public override async Task<MemberDto> UpdateAsync(Guid id, UpdateMemberDto input)
        {
            var oldMemberData = await this.BeforeUpdateAsync(input);
            var oldMemberEmail = oldMemberData.MemberEmail;
            var result = await base.UpdateAsync(id , input);
            await this.AfterUpdateAsync(result, oldMemberEmail);
            return result;

        }
        ///<summary>
        /// AfterUpdateAsync :Handel the needed operation after updating the member 
        /// updatig member email and username in user table if the member mail is changed
        ///</summary>
        ///<param name="input"> MemberDTO</param>
        ///<param name="oldMemberEmail">the old mail of the member</param>
        /// <returns></returns>
        private async Task AfterUpdateAsync(MemberDto input, string oldMemberEmail)
        {
            //user mail update
            if (input.MemberEmail != oldMemberEmail)
            {
                var relatedUser = await _userManager.FindByIdAsync(input.UserId.ToString());
                await _userManager.SetEmailAsync(relatedUser, input.MemberEmail);
                await _userManager.SetUserNameAsync(relatedUser, input.MemberName);
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// BeforeDeleteAsync :Handel the verification need before deleting a member
        /// </summary>
        /// <param name="id">Guid : member Id</param>
        /// <returns>Member : the exsiting member in database </returns>
        private async Task<Member> BeforeDeleteAsync(Guid id)
        {
            var member = await this.Repository.FindAsync(m => m.Id == id);
            if(member == null)
                throw new UserFriendlyException(string.Format(TodoListDomainErrorCodes.TODOLIST_MEMBER_WITH_ID_NOT_FOUND), id.ToString());
            return member;

        }
        /// <summary>
        /// DeleteAsync :Handel the deletion of the member and calls the befor and after function
        /// </summary>
        /// <param name="id" >Guid:member Id</param>
        /// <returns></returns>
        [Authorize(TodoListPermissions.TodoListMembers.Update)]
        public override async Task DeleteAsync(Guid id)
        {
            var member =await this.BeforeDeleteAsync(id);
            await base.DeleteAsync(id);
            await this.AfterDeleteAsync(member.UserId);
        }
        ///<summary>
        /// AfterDeleteAsync :Handel the needed operation after deleting the member 
        /// deleting  the user related to the member from user table 
        ///</summary>
        ///<param name="userId"> Guid :realted member userId</param>
        /// <returns></returns>
        private async Task AfterDeleteAsync(Guid userId)
        {
            var relatedUser = await _userManager.GetByIdAsync(userId);
            await _userManager.DeleteAsync(relatedUser);
        }
        #endregion



    }
}
