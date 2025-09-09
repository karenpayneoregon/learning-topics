using System.Linq.Expressions;
using PartialSamples1.Interfaces;
using PartialSamples1.Models;

namespace PartialSamples1.Classes;
/// <summary>
/// Provides operations for managing <see cref="Company"/> entities, including 
/// retrieval, addition, updating, and removal of records. Implements the 
/// <see cref="IOperations{T}"/> interface to ensure consistency in operations 
/// for <see cref="Company"/> objects.
/// </summary>
/// <remarks>
/// A simple example of using a partial interface.
/// </remarks>
public class CompanyOperations : IOperations<Company>
{
    public Task<List<Company>> GetAllAsync() => throw new NotImplementedException();
    public Task<Company> GetByIdAsync(int id) => throw new NotImplementedException();
    public Task<Company> GetByIdWithIncludesAsync(int id) => throw new NotImplementedException();
    public Task<int> SaveAsync() => throw new NotImplementedException();
    public Task<Company> SelectAsync(Expression<Func<Company, bool>> predicate) => throw new NotImplementedException();
    public IEnumerable<Company> GetAll()
    {
        throw new NotImplementedException();
    }

    public Company GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Company GetByIdWithIncludes(int id)
    {
        throw new NotImplementedException();
    }

    public bool Remove(int id)
    {
        throw new NotImplementedException();
    }

    public void Add(in Company sender)
    {
        throw new NotImplementedException();
    }

    public void Update(in Company sender)
    {
        throw new NotImplementedException();
    }

    public int Save()
    {
        throw new NotImplementedException();
    }

    public Company Select(Expression<Func<Company, bool>> predicate)
    {
        throw new NotImplementedException();
    }
}