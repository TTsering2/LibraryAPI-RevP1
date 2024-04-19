using Libraries.Models;

namespace Libraries.Services;
public interface IGenreService 
{
    Task<IEnumerable<Genre>> GetAllGenresAsync();
    Task<Genre> GetAGenreAsync(int genreId);
    Task UpdateAGenreAsync(int genreId, Genre updatedGenre);
    Task DeleteAGenreAsync(int genreId);
    Task<Genre> CreateGenreAsync(Genre genre);
}