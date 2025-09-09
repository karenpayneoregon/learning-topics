using System.Linq.Expressions;

namespace PartialSamples1.Interfaces;

public partial interface IOperations<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    T GetByIdWithIncludes(int id);
    bool Remove(int id);
    void Add(in T sender);
    void Update(in T sender);
    int Save();
    public T Select(Expression<Func<T, bool>> predicate);
}