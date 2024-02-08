using Microsoft.VisualBasic;
using Scriban.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Entities.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace TodoList.Data
{
    public class TodolisDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<task , Guid > _todoItemRepository;

        public TodolisDataSeedContributor(IRepository<task, Guid> todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _todoItemRepository.CountAsync() > 0)
            {
                return;
            }
            var item1 = new task(
    title: "Sample Task",
    description: "Description of the task",
    dueDate: DateTime.Now.AddDays(7),
    completed: false,
    userId: Guid.NewGuid(),
    userName: "User2",
userEmail: "email2"
);
            var item2 = new task(
title: "adding features ",
description: "Description of the task",
dueDate: DateTime.Now.AddDays(7),
completed: false,
userId: Guid.NewGuid(),
userName : "User2",
userEmail: "email2"

);
            await _todoItemRepository.InsertManyAsync(new[] { item1, item2 });
        }
    }
}
