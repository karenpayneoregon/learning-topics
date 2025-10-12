using AddImagesFromFilesApp.Models;
using System.Data;
using AddImagesFromFilesApp.Classes.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;
using Serilog;

namespace AddImagesFromFilesApp.Classes;
public class CategoryRepository
{
    private readonly IDbConnection _connection = new SqlConnection(DataConnections.Instance.MainConnection);

    /// <summary>
    /// Resets the "Categories" table in the database by deleting all records
    /// and reseeding the identity column to start from 0.
    /// </summary>
    /// <remarks>
    /// This method executes two SQL commands:
    /// 1. Deletes all rows from the "Categories" table.
    /// 2. Resets the identity seed of the "Categories" table to 0.
    /// </remarks>
    /// <exception cref="SqlException">
    /// Thrown if there is an issue executing the SQL commands on the database.
    /// </exception>
    public void ResetTable()
    {
        _connection.Execute("DELETE FROM dbo.Categories");
        _connection.Execute("DBCC CHECKIDENT (Categories, RESEED, 0)");
    }

    /// <summary>
    /// Retrieves all categories from the "Categories" table in the database asynchronously.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an 
    /// <see cref="IEnumerable{T}"/> of <see cref="Category"/> objects, ordered by the category name.
    /// </returns>
    /// <exception cref="SqlException">
    /// Thrown if there is an issue executing the SQL query on the database.
    /// </exception>
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        const string sql = 
            """
            SELECT CategoryID, CategoryName, Description, Photo
            FROM dbo.Categories
            ORDER BY CategoryName
            """;

        return await _connection.QueryAsync<Category>(sql);
    }

    /// <summary>
    /// Retrieves all categories from the "Categories" table in the database asynchronously
    /// and returns the data as a <see cref="DataTable"/>.
    /// </summary>
    /// <remarks>
    /// The resulting <see cref="DataTable"/> will have its "CategoryID" column hidden
    /// by setting its <see cref="MappingType"/> to <see cref="MappingType.Hidden"/>.
    /// </remarks>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a 
    /// <see cref="DataTable"/> populated with the category data, ordered by the category name.
    /// </returns>
    /// <exception cref="SqlException">
    /// Thrown if there is an issue executing the SQL query on the database.
    /// </exception>
    public async Task<DataTable> GetAllDataTableAsync()
    {
        const string sql =
            """
            SELECT CategoryID, CategoryName, Description, Photo
            FROM dbo.Categories
            ORDER BY CategoryName
            """;

        var reader = await _connection.ExecuteReaderAsync(sql);
        var table = new DataTable();
        table.Load(reader);
        
        table.Columns["CategoryID"]!.ColumnMapping = MappingType.Hidden;
        
        return table;
    }

    /// <summary>
    /// Retrieves a category from the "Categories" table in the database asynchronously by its unique identifier.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the category to retrieve.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the 
    /// <see cref="Category"/> object if found; otherwise, <c>null</c>.
    /// </returns>
    /// <exception cref="SqlException">
    /// Thrown if there is an issue executing the SQL query on the database.
    /// </exception>
    public async Task<Category?> GetByIdAsync(int id)
    {
        const string sql = 
            """
            SELECT CategoryID, CategoryName, Description, Photo
            FROM dbo.Categories
            WHERE CategoryID = @CategoryID
            """;

        return await _connection.QueryFirstOrDefaultAsync<Category>(sql, new { CategoryID = id });
    }

    /// <summary>
    /// Inserts a new category into the "Categories" table in the database asynchronously.
    /// </summary>
    /// <param name="category">
    /// The <see cref="Category"/> object containing the details of the category to be inserted.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the 
    /// ID of the newly inserted category.
    /// </returns>
    /// <exception cref="SqlException">
    /// Thrown if there is an issue executing the SQL command on the database.
    /// </exception>
    public async Task<int> InsertAsync(Category category)
    {
        const string sql = 
            """
            INSERT INTO dbo.Categories (CategoryName, Description, Photo)
            VALUES (@CategoryName, @Description, @Photo);
            SELECT CAST(SCOPE_IDENTITY() as int);
            """;

        return await _connection.ExecuteScalarAsync<int>(sql, category);
    }

    /// <summary>
    /// Inserts multiple categories into the "Categories" table in the database asynchronously.
    /// </summary>
    /// <param name="categories">
    /// A collection of <see cref="Category"/> objects to be inserted into the database.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result is a tuple containing:
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// <c>affected</c>: The number of rows affected by the operation.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// <c>bool</c>: A boolean value indicating whether the operation was successful.
    /// </description>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// This method uses a transaction to ensure atomicity. If the operation fails, the transaction is rolled back.
    /// </remarks>
    /// <exception cref="SqlException">
    /// Thrown if there is an issue executing the SQL commands on the database.
    /// </exception>
    public async Task<(int affected, bool)> InsertManyAsync(IEnumerable<Category> categories)
    {
        const string sql = 
            """
            INSERT INTO dbo.Categories (CategoryName, Description, Photo)
            VALUES (@CategoryName, @Description, @Photo);
            """;
        
        _connection.Open();
        
        using var transaction = _connection.BeginTransaction();
        try
        {
            var affected = await _connection.ExecuteAsync(sql, categories, transaction);
            transaction.Commit();
            return (affected, true);
        }
        catch(Exception ex)
        {
            transaction.Rollback();
            Log.Error(ex, "Error inserting multiple categories.");
            return (0, false);
        }
    }

    /// <summary>
    /// Updates an existing category in the "Categories" table in the database.
    /// </summary>
    /// <param name="category">
    /// The <see cref="Category"/> object containing the updated information for the category.
    /// The <see cref="Category.CategoryID"/> property must match the ID of the category to be updated.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the number of rows affected by the update operation.
    /// </returns>
    /// <exception cref="SqlException">
    /// Thrown if there is an issue executing the SQL update command on the database.
    /// </exception>
    public async Task<int> UpdateAsync(Category category)
    {
        const string sql = 
            """
            UPDATE dbo.Categories
            SET CategoryName = @CategoryName,
                Description = @Description,
                Photo = @Photo
            WHERE CategoryID = @CategoryID
            """;

        return await _connection.ExecuteAsync(sql, category);
    }

    /// <summary>
    /// Deletes a category from the "Categories" table in the database asynchronously 
    /// based on the specified category ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the category to be deleted.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains 
    /// the number of rows affected by the delete operation.
    /// </returns>
    /// <exception cref="SqlException">
    /// Thrown if there is an issue executing the SQL command on the database.
    /// </exception>
    public async Task<int> DeleteAsync(int id)
    {
        const string sql = "DELETE FROM dbo.Categories WHERE CategoryID = @CategoryID";
        return await _connection.ExecuteAsync(sql, new { CategoryID = id });
    }
}