using Libraries.DTOs;
using Libraries.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
// using Microsoft.AspNetCore.JsonPatch;


namespace Libraries.Data;

public interface IBooksRepository
{
    Task<BookDTO> GetBookByIdAsync(int bookId);
    Task<IEnumerable<BookDTO>> GetAllBooksAsync();
    Task<BookDTO> CreateBookAsync(BookCreateDTO bookDto);
    Task UpdateBooksAsync(int bookId, UpdateBookDTO patchDocument);
    Task DeleteBookAsync(int bookId);
    Task <IEnumerable<BookDTO>> GetBooksByAuthorNameAsync(string authorName);
    Task <IEnumerable<BookDTO>> GetBooksByGenreNameAsync(string genreName);

}