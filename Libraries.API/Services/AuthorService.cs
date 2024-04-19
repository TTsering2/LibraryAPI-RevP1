using Libraries.Models;
using Libraries.Data;

namespace Libraries.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorsRepository _repository;

    public AuthorService(IAuthorsRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        return await _repository.GetAllAuthorsAsync();
    }

    public async Task<Author> GetAnAuthorAsync (int authorId)
    {
        return await _repository.GetAGenreAsync(authorId);
    }

    public async Task UpdateAnAuthorAsync(int authorId, Author updatedAuthor)
    {
        await _repository.UpdateAnAuthorAsync(authorId, updatedAuthor);
    }
    public async Task DeleteAnAuthorAsync(int authorId)
    {
        await _repository.DeleteAnAuthorAsync(authorId);
    }
    public async Task<Author> CreateAnAuthorAsync(Author author)
    {
        return await _repository.CreateAnAuthorAsync(author);
    }
}