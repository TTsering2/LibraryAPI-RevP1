using System.Linq;
using System.Threading.Tasks;
using Libraries.Models;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Data;

public class GenresRepository : IGenresRepository
{
    private readonly LibrariesDbContext _context;
    public GenresRepository(LibrariesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Genre>> GetAllGenresAsync()
    {
        List<Genre> genres = await _context.Genres.ToListAsync();
        return genres;
    }

    public async Task UpdateAGenreAsync(int genreId, Genre updatedGenre)
    {
        if(updatedGenre == null)
        {
            throw new ArugumentNullException(nameof(updatedGenre));
        }

        Genre genre = await _context.Genres,FindAsync(genreId);

        if(genre == null)
        {
            genre.GenreName = updatedGenre.GenreName;
        }

        if (!string.IsNullOrWhiteSpace(updatedGenre.GenreName))
        {
            genre.GenreName = updatedGenre.GenreName;
        }

        _context.Entry(genre).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAGenre(int genreId){
        Genre genre = await _context.Genres.FindAsync(genreId);

        if(book == null){
            return;
        }

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
    }

    public async Task<Genre> CreateGenreAsync(Genre newGenre){
        if(newGenre == null){
            throw new ArgumentNullException(nameof(newGenre));
        }

        Genre existGenre = await _context.Genres.FirstOrDefaultAsync(g=>g.GenreName == newGenre.genreName);

        if(existGenre != null)
        {
            throw new InvalidOperationException($"Genre '{newGenre.GenreName}' already exists");
        }

        _context.Genres.Add(newGenre);
        await _context.SaveChangesAsync();

        return newGenre;
    }
}