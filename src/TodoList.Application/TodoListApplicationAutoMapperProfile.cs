using AutoMapper;
using TodoList.Dtos.TaskDtos;
using TodoList.Entities.Tasks;

namespace TodoList;

public class TodoListApplicationAutoMapperProfile : Profile
{
    public TodoListApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<task, TaskDto>();
        CreateMap<task,UpdateTaskDto>();
        CreateMap<CreateTaskDto, task>();

    }
}
