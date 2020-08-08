using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IItemChoiceService : IBaseEntityService<ItemChoice>
    {
        public IEnumerable<ItemChoice> GetAllByUser(object? userId, bool noTracking = true);

        public Task<IEnumerable<ItemChoice>> GetAllByUserAsync(object? userId, bool noTracking = true);
    }
}