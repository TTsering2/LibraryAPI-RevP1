using Libraries.DTOs;
using Libraries.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Libraries.Data;

public interface IBooksRepository
{
    Task<BookDTO> GetBookByIdAsync(int bookId);
    Task<IEnumerable<BookDTO>> GetAllBooksAsync();
    Task<BookDTO> CreateBookAsync(BookCreateDTO bookDto);
    Task UpdateBooksAsync(int bookId, UpdateBookDTO bookDto);
    Task DeleteBookAsync(int bookId);
}