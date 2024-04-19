using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Libraries.DTOs;
using Libraries.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Data;
using Libraries.Models;

namespace Books.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase 
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService){
        _bookService = bookService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookDTO>> GetBookById(int id){
        try{
            BookDTO book = await _bookService.GetBookByIdAsync(id);

            if(book == null){
                return NotFound();
            }
            return book;
        } catch (Exception ex){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving book: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllBooks(){
        try {
            IEnumerable<BookDTO> books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        } catch (Exception ex){
             return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving books: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<BookDTO>> CreateBook(BookCreateDTO bookDto){
        try{
            BookDTO createdBook = await _bookService.CreateBookAsync(bookDto);
            return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
        } catch (Exception ex){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating book: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBook(int id, UpdateBookDTO bookDto){
        try{
            await _bookService.UpdateBooksAsync(id, bookDto);
            return NoContent(); // Return 204 No Content for successful update
        }
        catch (Exception ex){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating book: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBook(int id){
        try{
            await _bookService.DeleteBookAsync(id);
            return NoContent(); // Return 204 No Content for successful deletion
        }catch (Exception ex){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting book: {ex.Message}");
        }
    }
}