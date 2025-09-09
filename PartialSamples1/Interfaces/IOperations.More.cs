using System.Linq.Expressions;

namespace PartialSamples1.Interfaces;
public partial interface IOperations<T>
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> GetByIdWithIncludesAsync(int id);
    Task<int> SaveAsync();
    public Task<T> SelectAsync(Expression<Func<T, bool>> predicate);
}