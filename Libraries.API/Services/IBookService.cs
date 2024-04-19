using Libraries.Models;
using Libraries.DTOs;


namespace Libraries.Services;

public interface IBookService 
{
    Task<BookDTO> GetBookByIdAsync(int bookId);
    Task<IEnumerable<BookDTO>> GetAllBooksAsync();
    Task<BookDTO> CreateBookAsync(BookCreateDTO bookDto);
    Task UpdateBooksAsync(int bookId, UpdateBookDTO patchDocument);

    Task DeleteBookAsync(int bookId);
    Task <IEnumerable<BookDTO>> GetBooksByAuthorNameAsync(string authorName);
    Task <IEnumerable<BookDTO>> GetBooksByGenreNameAsync(string genreName);
}