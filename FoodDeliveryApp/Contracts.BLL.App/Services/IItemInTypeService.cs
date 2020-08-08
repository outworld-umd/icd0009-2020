using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IItemInTypeService : IBaseEntityService<ItemInType>
    {
        public IEnumerable<ItemInType> GetAllByUser(object? userId, bool noTracking = true);

        public Task<IEnumerable<ItemInType>> GetAllByUserAsync(object? userId, bool noTracking = true);
    }
}