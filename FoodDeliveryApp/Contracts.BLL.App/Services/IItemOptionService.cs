using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.anguzo.Contracts.BLL.Base.Services;namespace Contracts.BLL.App.Services
{
    public interface IItemOptionService : IBaseEntityService<ItemOption>
    {
        public IEnumerable<ItemOption> GetAllByUser(object? userId, bool noTracking = true);

        public Task<IEnumerable<ItemOption>> GetAllByUserAsync(object? userId, bool noTracking = true);
    }
}