using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models;

public class Movies
{
    public int Id { get; set; }
    public string? Title { get; set; }
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    public string? Genre { get; set; }
    public decimal Price { get; set; }

    public string ProfileFile { get; set; }

    internal static IQueryable<Movies> Where(Func<object, object> value)
    {
        throw new NotImplementedException();
    }
}