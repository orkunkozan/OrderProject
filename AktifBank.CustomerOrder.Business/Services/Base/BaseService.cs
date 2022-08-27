using AktifBank.CustomerOrder.DataAccess.Contexts;

namespace AktifBank.CustomerOrder.Business.Services.Base
{
    public class BaseService
    {
        protected readonly ProjectDbContext _dataContext;
        public BaseService(ProjectDbContext dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
