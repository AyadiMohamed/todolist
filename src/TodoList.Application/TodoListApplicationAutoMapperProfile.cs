using AutoMapper;
using TodoList.Dtos.MemberDos;
using TodoList.Dtos.TaskDtos;
using TodoList.Entities.Members;
using TodoList.Models.appTasks;

namespace TodoList;

public class TodoListApplicationAutoMapperProfile : Profile
{
    public TodoListApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<task, TaskDto>();
        CreateMap<CreateTaskDto, task>();
        CreateMap<UpdateTaskDto, task>();
        CreateMap<task, TaskDtoListing>();




        CreateMap<Member, MemberDto>();
        CreateMap<CreateMemberDto, Member>()
            .ForMember(member => member.UserId, opt => opt.MapFrom(src => src.GetUserId()));
        CreateMap<UpdateMemberDto, Member>();
        CreateMap<Member, MemberListingDto>();


    }
}
