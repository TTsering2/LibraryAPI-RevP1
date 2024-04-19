using Libraries.Models;
using Libraries.API.DTOs;

namespace Libraries.API.Services;

public interface IBookService 
{
    Task<BookDTO> GetBookByIdAsync(int bookId);
    Task<IEnumerable<BookDTO>> GetAllBooksAsync();
    Task<BookDTO> CreateBookAsync(BookCreateDTO bookDto);
    Task UpdateBooksAsync(int bookId, UpdateBookDTO bookDto);
    Task DeleteBookAsync(int bookId);
}