

namespace TCE.Repository.Interfaces
{
    public interface IContextManager
    {
        IDbContext GetContext();        
    }
}
