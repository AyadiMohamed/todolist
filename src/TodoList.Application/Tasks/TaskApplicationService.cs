using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using TodoList.Dtos.TaskDtos;
using TodoList.Entities.Tasks;
using TodoList.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp;



namespace TodoList.Tasks
{

    public class TaskApplicationService : CrudAppService<task,
                                                   TaskDto,
                                                   TaskDtoListing,
                                                   Guid,
                                                   PagedAndSortedResultRequestDto,
                                                   CreateTaskDto,
                                                   UpdateTaskDto>
    {
        private IdentityUserManager _userManager;
        private IdentityRoleManager _roleManager;
        public TaskApplicationService(IRepository<task, Guid> repository, IdentityRoleManager roleManager, IdentityUserManager userManager) : base(repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region GetAsync
        [Authorize(TodoListPermissions.TodoListTasks.Read)]
        public override async Task<TaskDto> GetAsync(Guid id)
        {
            var result = await base.GetAsync(id);
            return result;
        }
        #endregion

        #region GetListAsync
        [Authorize(TodoListPermissions.TodoListTasks.Read)]
        public override async Task<PagedResultDto<TaskDtoListing>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var result = await base.GetListAsync(input);
            return result;
        }
        #endregion

        #region CreateTask
        /// <summary>
        /// BeforeCreateAsync : Handling the verification before creating new task
        /// and creating the user related to that task
        /// </summary>
        private async Task<Guid> BeforeCreateAsync(CreateTaskDto input)
        {
            // task existins verification
            var task = await this.Repository.FirstOrDefaultAsync(m => m.Title == input.Title);
            if (task != null)
                throw new UserFriendlyException("Task Already exits");

            //user creation

            var user = new IdentityUser(GuidGenerator.Create(), input.UserName, input.UserEmail);
            var createUserResult = await _userManager.CreateAsync(user);
            if (!createUserResult.Succeeded)
                throw new UserFriendlyException("User Already exits");
            var roleExist = await _roleManager.RoleExistsAsync("User");
            if (!roleExist)
                await _roleManager.CreateAsync(new IdentityRole(GuidGenerator.Create(), "User"));
            await _userManager.AddToRoleAsync(user, "User");
            await _userManager.AddPasswordAsync(user, "P@ssw0rd**");
            return user.Id;
        
        }
        /// <summary>
        /// CreateAsync :Handel the creation of the task and calls the befor and after function
        /// </summary>
        /// <param name="input" >CreateTaskDto</param>
        /// <returns></returns>
        [Authorize(TodoListPermissions.TodoListTasks.Create)]
        public override async Task<TaskDto> CreateAsync(CreateTaskDto input)
        {
            var userId = await this.BeforeCreateAsync(input);
            input.SetUserId(userId);
            var result = await base.CreateAsync(input);
            return result;
        }

        #endregion

        #region UpdateAsyn
        /// <summary>
        /// BeforeUpdateAsync :Handel the verification need before updating a task
        /// </summary>
        /// <param name="input">UpdateTaskDto</param>
        /// <returns>task: the exsiting task in database </returns>
        public async Task<task> BeforeUpdateAsync(UpdateTaskDto input)
        {
            var task = await this.Repository.FindAsync(input.Id);
            if (task == null)
                throw new UserFriendlyException("task do not exits");
            return task;

        }

        /// <summary>
        /// UpdateAsync :Handel the update of the task and calls the befor and after function
        /// </summary>
        /// <param name="id" >Guid:task Id</param>
        /// <param name="input" >UpdateTaskDto</param>
        /// <returns>taskDto</returns>
        public override async Task<TaskDto> UpdateAsync(Guid id , UpdateTaskDto input) {
            var oldTask = await this.BeforeUpdateAsync(input);
            var oldUserName = oldTask.UserName;
            var oldUserEmail = oldTask.UserEmail;
            var result = await base.UpdateAsync(id, input);
            await this.AfterUpdateAsync(result, oldUserName , oldUserEmail);
            return result;
        }
        ///<summary>
        /// AfterUpdateAsync :Handel the needed operation after updating the task 
        /// updatig  email and username in user table if the userName in task and mail are changed
        ///</summary>
        ///<param name="input"> TaskDto</param>
        ///<param name="oldUserEmail">the old mail of the user</param>
        /// <returns></returns>
        public async Task AfterUpdateAsync(TaskDto input, string OldUserName, string OldUserEmail)
        {
            if (input.UserName != OldUserName && input.UserEmail != OldUserEmail)
            {
                var relatedUser = await _userManager.FindByIdAsync(input.UserId.ToString());
                await _userManager.SetEmailAsync(relatedUser, input.UserEmail);
                await _userManager.SetUserNameAsync(relatedUser, input.UserName);
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// BeforeDeleteAsync :Handel the verification need before deleting a task
        /// </summary>
        /// <param name="id">Guid : task Id</param>
        /// <returns>Member : the exsiting task in database </returns>
        private async Task<task> BeforeDeleteAsync(Guid id)
        {
            var task = await this.Repository.FindAsync(m => m.Id == id);
            if (task == null)
                throw new UserFriendlyException("task do not exits");
            return task;
        }
        /// <summary>
        /// DeleteAsync :Handel the deletion of the task and calls the befor and after function
        /// </summary>
        /// <param name="id" >Guid:task Id</param>
        /// <returns></returns>
        public override async Task DeleteAsync(Guid id)
        {
            var task = await this.BeforeDeleteAsync(id);
            await base.DeleteAsync(id);
            await this.AfterDeleteAsync(task.UserId);
        }
        ///<summary>
        /// AfterDeleteAsync :Handel the needed operation after deleting the task
        /// deleting  the user related to the task from user table 
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
