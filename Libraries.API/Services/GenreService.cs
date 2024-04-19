using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Models;
using Libraries.Data;

namespace Libraries.Services

public class GenreService : IGenreService
{
    private readonly IGenresRepository _repository;

    public GenreService(IGenresRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Genre>> GetAllGenresAsync()
    {
        return await _repository.GetAllGenresAsync();
    }

    public async Task<Genre> GetAGenreAsync(int genreId)
    {
        return await _repository.GetAGenreAsync(genreId);
    }

    public async Task UpdateAGenreAsync(int genreId, Genre updatedGenre)
    {
        await _repository.UpdateAGenreAsync(genreId, updatedGenre);
    }

    public async Task DeleteAGenreAsync(int genreId)
    {
        await _repository.DeleteAGenreAsync(genreId);
    }

    public async Task<Genre> CreateGenreAsync(Genre genre)
    {
        return await _repository.CreateGenreAsync(genre);
    }
}

