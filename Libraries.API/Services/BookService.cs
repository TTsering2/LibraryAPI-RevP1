using Libraries.DTOs;
using Libraries.Models;
using Libraries.Data;
// using Microsoft.AspNetCore.JsonPatch;


namespace Libraries.Services;

public class BookService : IBookService
{
    private readonly IBooksRepository _repository;
    public BookService(IBooksRepository repository)
    {
        _repository = repository;
    }

    public async Task<BookDTO> GetBookByIdAsync(int bookId){
        return await _repository.GetBookByIdAsync(bookId);
    }

    public async Task<IEnumerable<BookDTO>> GetAllBooksAsync(){
        return await _repository.GetAllBooksAsync();
    }

    public async Task<BookDTO> CreateBookAsync(BookCreateDTO bookDto){
        return await _repository.CreateBookAsync(bookDto);
    }

    public async Task UpdateBooksAsync(int bookId, UpdateBookDTO patchDocument)
    {
        await _repository.UpdateBooksAsync(bookId, patchDocument);
    }

    public async Task DeleteBookAsync(int bookId){
        await _repository.DeleteBookAsync(bookId);
    }

    public async Task <IEnumerable<BookDTO>> GetBooksByAuthorNameAsync(string authorName){
       return await _repository.GetBooksByAuthorNameAsync(authorName);
    }
      public async Task <IEnumerable<BookDTO>> GetBooksByGenreNameAsync(string genreName){
        return await _repository.GetBooksByGenreNameAsync(genreName);
    }
}