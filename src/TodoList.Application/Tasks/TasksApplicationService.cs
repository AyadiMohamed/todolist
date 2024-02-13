using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using TodoList.Dtos.TaskDtos;
using TodoList.Entities.Tasks;
using TodoList.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;
using TodoList.Entities.Members;
using System.Collections.Generic;
using EmailSender.EmailSender;



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
        private IRepository<Member, Guid> _memberRepository;
        private readonly EmailSendingService _emailSender;


        public TaskApplicationService(
            IRepository<task, Guid> repository,
            IRepository<Member, Guid> memberRepository, EmailSendingService emailSender
           ) : base(repository)
        {
            _memberRepository = memberRepository;
            _emailSender = emailSender;
        }

        #region GetAsync
        [Authorize(TodoListPermissions.TodoListTasks.Read)]
        public override async Task<TaskDto> GetAsync(Guid id)
        {
            var entity = await this.Repository.GetAsync(id);
            if (entity == null)
                throw new UserFriendlyException(string.Format(TodoListDomainErrorCodes.TODOLIST_TASK_WITH_ID_NOT_FOUND), id.ToString());
            var result = await MapToGetOutputDtoAsync(entity);
            return result;
        }
        #endregion

        #region GetListAsync
        [Authorize(TodoListPermissions.TodoListTasks.Read)]
        public override async Task<PagedResultDto<TaskDtoListing>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var query = await this.Repository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, true);
            var totalCount = await this.Repository.CountAsync();
            var entityDtos = new List<TaskDtoListing>();
            if (totalCount > 0)
            {
                entityDtos = await MapToGetListOutputDtosAsync(query);
            }
            return new PagedResultDto<TaskDtoListing>(totalCount, entityDtos);
        }
        #endregion

        #region CreateTask
        /// <summary>
        /// BeforeCreateAsync : Handling the verification before creating new task
        /// and creating the user related to that task
        /// </summary>
        private async Task BeforeCreateAsync(CreateTaskDto input)
        {
            //memberId verification
            if (!input.MemberId.HasValue)
            {
                input.MemberId = this.CurrentUser.Id;
            }
            else
            {
                //role verification
                if (!this.CurrentUser.IsInRole(TodoListConsts.AdminRole))
                    throw new UserFriendlyException(TodoListDomainErrorCodes.TODOLIST_CANT_ASSIGN_TASKS_TO_OTHER);
                // member verfication
                var member = await _memberRepository.FirstOrDefaultAsync(x => x.UserId == input.MemberId);
                if (member == null)
                    throw new UserFriendlyException(string.Format(TodoListDomainErrorCodes.TODOLIST_MEMBER_WITH_ID_NOT_FOUND), input.Id.ToString());

                input.SetUserId((Guid)member.UserId);
                await _emailSender.SendEmailAsync(/*member.MemberEmail, "New Task Created", "A new task has been created."*/);

            }
        }
        /// <summary>
        /// CreateAsync :Handel the creation of the task and calls the befor and after function
        /// </summary>
        /// <param name="input" >CreateTaskDto</param>
        /// <returns></returns>
        [Authorize(TodoListPermissions.TodoListTasks.Create)]
        public override async Task<TaskDto> CreateAsync(CreateTaskDto input)
        {
            await this.BeforeCreateAsync(input);
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
        private async Task BeforeUpdateAsync(UpdateTaskDto input)
        {
            // member verfication
            var member = await _memberRepository.FirstOrDefaultAsync(x => x.UserId == input.MemberId);
            if (member == null)
                throw new UserFriendlyException(string.Format(TodoListDomainErrorCodes.TODOLIST_MEMBER_WITH_ID_NOT_FOUND), input.Id.ToString());

        }

        /// <summary>
        /// UpdateAsync :Handel the update of the task and calls the befor and after function
        /// </summary>
        /// <param name="id" >Guid:task Id</param>
        /// <param name="input" >UpdateTaskDto</param>
        /// <returns>taskDto</returns>
        [Authorize(TodoListPermissions.TodoListTasks.Update)]
        public override async Task<TaskDto> UpdateAsync(Guid id, UpdateTaskDto input)
        {
            await this.BeforeUpdateAsync(input);
            var result = await base.UpdateAsync(id, input);
            return result;
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
                throw new UserFriendlyException(TodoListDomainErrorCodes.TODOLIST_TASK_WITH_ID_NOT_FOUND);
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
        }
        #endregion

        #region MarkAsCompleted
        /// <summary>
        /// DeleteAsync :Change the completed property of a task
        /// </summary>
        /// <param name="id" >Guid:task Id</param>
        /// <returns></returns>
        public async Task MarkAsCompleted(Guid id)
        {
            var task = await this.Repository.FindAsync(m => m.Id == id);
            //role verification
            if (!this.CurrentUser.IsInRole(TodoListConsts.AdminRole))
                throw new UserFriendlyException(TodoListDomainErrorCodes.TODOLIST_CANT_ASSIGN_TASKS_TO_OTHER);
            if (task == null)
                throw new UserFriendlyException(TodoListDomainErrorCodes.TODOLIST_TASK_WITH_ID_NOT_FOUND);
            task.Completed = true;
        }
        #endregion


    }
}
