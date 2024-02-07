using Scriban.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Entities;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace TodoList.Data
{
    public class TodolisDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Entities.Task, Guid> _todoItemRepository;

        public TodolisDataSeedContributor(IRepository<Entities.Task, Guid> todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }
        public async System.Threading.Tasks.Task SeedAsync(DataSeedContext context)
        {
            if (await _todoItemRepository.CountAsync() > 0)
            {
                return;
            }
            var item1 = new Entities.Task
            {
                Text = "fixing bugs"
            };
            var item2 = new Entities.Task
            {
                Text = "Adding features"
            };

            await _todoItemRepository.InsertManyAsync(new[] { item1, item2 });
        }
    }
}
