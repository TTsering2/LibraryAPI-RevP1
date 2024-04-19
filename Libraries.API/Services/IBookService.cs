using Libraries.Models;
using Libraries.DTOs;

namespace Libraries.Services;

public interface IBookService 
{
    Task<BookDTO> GetBookByIdAsync(int bookId);
    Task<IEnumerable<BookDTO>> GetAllBooksAsync();
    Task<BookDTO> CreateBookAsync(BookCreateDTO bookDto);
    Task UpdateBooksAsync(int bookId, UpdateBookDTO bookDto);
    Task DeleteBookAsync(int bookId);
}