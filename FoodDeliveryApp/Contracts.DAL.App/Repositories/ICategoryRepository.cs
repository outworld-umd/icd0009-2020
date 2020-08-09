using System.Threading.Tasks;
using ee.itcollege.anguzo.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories {

    public interface ICategoryRepository : IBaseRepository<Category>
    {
        public new Task<Category> UpdateAsync(Category entity, object? userId = null);

    }

}