using AutoMapper;
using TodoList.Entities;
using TodoList.TodoItems;

namespace TodoList;

public class TodoListApplicationAutoMapperProfile : Profile
{
    public TodoListApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Task,todoItemDTO>();
    }
}
