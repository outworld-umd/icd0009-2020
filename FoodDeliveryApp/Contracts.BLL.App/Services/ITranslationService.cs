using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.Domain.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface ITranslationService : IBaseEntityService<Translation>
    {
        public Task<IEnumerable<Translation>> GetAllAsyncWithoutDefault(object? userId = null, bool noTracking = true);
        new Translation Add(Translation entity);
    }
}