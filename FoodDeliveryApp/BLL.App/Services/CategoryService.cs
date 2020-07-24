using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class CategoryService : BaseEntityService<IAppUnitOfWork, ICategoryRepository, ICategoryServiceMapper, DAL.App.DTO.Category, Category>, ICategoryService
    {
        public CategoryService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.Categories, new CategoryServiceMapper())
        {
        }
    }
}