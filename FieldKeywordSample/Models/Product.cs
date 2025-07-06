
// ReSharper disable ConvertToAutoProperty

namespace FieldKeywordSample.Models;


internal class Product 
{
    public int Id
    {
        get => field;
        set => field = value;
    }
    public string Name
    {
        get => field;
        set => field = value;
    }
    public decimal Price
    {
        get => field;
        set => field = value;
    }
    public int Stock
    {
        get => field;
        set => field = value;
    }
}
