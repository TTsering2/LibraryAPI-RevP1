using Libraries.DTOs;
using Libraries.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
// using Microsoft.AspNetCore.JsonPatch;


namespace Libraries.Data;

public interface IGenresRepository
{
    Task<IEnumerable<Genre>> GetAllGenresAsync();
    Task<Genre> GetAGenreAsync(int genreId);
    Task UpdateAGenreAsync(int genreId, Genre updatedGenre);
    Task DeleteAGenreAsync(int genreId);
    Task<Genre> CreateGenreAsync(Genre genre);
}