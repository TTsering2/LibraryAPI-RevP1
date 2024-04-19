using Libraries.DTOs;
using Libraries.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
// using Microsoft.AspNetCore.JsonPatch;


namespace Libraries.Data;

public interface IAuthorsRepository
{
    Task<IEnumerable<Author>> GetAllAuthorsAsync();
    Task<Author> GetAnAuthorAsync(int authorId);
    Task UpdateAnAuthorAsync(int authorId, Author updatedAuthor);
    Task DeleteAnAuthorAsync(int authorId);
    Task<Author> CreateAnAuthorAsync(Author author);
}