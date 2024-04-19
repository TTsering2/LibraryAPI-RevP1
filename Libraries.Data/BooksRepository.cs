using System.Linq;
using System.Threading.Tasks;
using Libraries.Models;
using Libraries.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;


namespace Libraries.Data;
public class BooksRepository : IBooksRepository 
    {
    private readonly LibrariesDbContext _context;

    public BooksRepository(LibrariesDbContext context)
    {
        _context = context;
    }

    public async Task<BookDTO> GetBookByIdAsync(int bookId)
    {
        BookDTO book = await _context.Books 
            .Where(b => b.Id == bookId)
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .Select(b => new BookDTO
            {
                Id = b.Id,
                Title = b.Title,
                Summary = b.Summary,
                AuthorName = b.Author.Name,
                GenreName = b.Genre.GenreName
            })
            .FirstOrDefaultAsync();

        return book; // Corrected return statement within the method block
    }

    public async Task<IEnumerable<BookDTO>> GetAllBooksAsync()
    {
        List<BookDTO> books = await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .Select(b => new BookDTO
            {
                Id = b.Id,
                Title = b.Title,
                Summary = b.Summary,
                AuthorName = b.Author.Name,
                GenreName = b.Genre.GenreName
            })
            .ToListAsync();

        return books;
        }

    public async Task<BookDTO> CreateBookAsync(BookCreateDTO bookDto)
    {
        Author author = await _context.Authors
            .FirstOrDefaultAsync(a => a.Name == bookDto.AuthorName)
            ?? new Author { Name = bookDto.AuthorName };

        Genre genre = await _context.Genres
            .FirstOrDefaultAsync(g => g.GenreName == bookDto.GenreName)
            ?? new Genre { GenreName = bookDto.GenreName };
            
            
        Book newBook = new Book
        {
            Title = bookDto.Title,
            Summary = bookDto.Summary,
            Author = author,
            Genre = genre
        };

        _context.Books.Add(newBook);
        await _context.SaveChangesAsync();

        return new BookDTO
        {
            Id = newBook.Id,
            Title = newBook.Title,
            Summary = newBook.Summary,
            AuthorName = newBook.Author.Name,
            GenreName = newBook.Genre.GenreName
        };
    }

        // public async Task UpdateBooksAsync(int bookId, UpdateBookDTO bookDto)
        // {
        //     Book book = await _context.Books.FindAsync(bookId);
        //     if (book == null)
        //     {
        //         return;
        //     }

        //     book.Title = bookDto.Title;
        //     book.Summary = bookDto.Summary;

        //     if(book.Author.Name != bookDto.AuthorName){
        //         Author author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == bookDto.AuthorName);

        //         if(author == null) {
        //             author = new Author { Name = bookDto.AuthorName};
        //             _context.Authors.Add(author);
        //         }

        //         book.Author = author;
        //     }

        //     if(!string.IsNullOrEmpty(bookDto.GenreName) && book.Genre?.GenreName != bookDto.GenreName) {
        //         Genre genre = await _context.Genres.FirstOrDefaultAsync(g => g.GenreName == bookDto.GenreName);
                
        //         if(genre == null) {
        //             genre = new Genre { GenreName = bookDto.GenreName };
        //             _context.Genres.Add(genre);
        //         }
        //         book.Genre = genre;
        //     }

        //     await _context.SaveChangesAsync();
        // }
    public async Task UpdateBooksAsync(int bookId, UpdateBookDTO updatedBookDto)
    {
        Book book = await _context.Books
            .Include(b => b.Author) // Ensure the author is included for comparison
            .Include(b => b.Genre)  // Ensure the genre is included for comparison
            .FirstOrDefaultAsync(b => b.Id == bookId);
        Console.WriteLine($"{book}");
        if (book == null)
        {
            throw new Exception($"Book with ID {bookId} not found.");
        }

        // Update book properties based on the provided DTO
        if (!string.IsNullOrEmpty(updatedBookDto.Title))
        {
            book.Title = updatedBookDto.Title;
        }
        if (!string.IsNullOrEmpty(updatedBookDto.Summary))
        {
            book.Summary = updatedBookDto.Summary;
        }
        if (!string.IsNullOrEmpty(updatedBookDto.AuthorName) && book.Author?.Name != updatedBookDto.AuthorName)
        {
            // Check if a different author already exists with this name, or create a new one
            Author existingAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Name == updatedBookDto.AuthorName);
            if (existingAuthor == null)
            {
                existingAuthor = new Author { Name = updatedBookDto.AuthorName };
                _context.Authors.Add(existingAuthor);
            }
            book.Author = existingAuthor;
        }
        if (!string.IsNullOrEmpty(updatedBookDto.GenreName) && book.Genre?.GenreName != updatedBookDto.GenreName)
        {
            // Check if a different genre already exists with this name, or create a new one
            Genre existingGenre = await _context.Genres.FirstOrDefaultAsync(g => g.GenreName == updatedBookDto.GenreName);
            if (existingGenre == null)
            {
                existingGenre = new Genre { GenreName = updatedBookDto.GenreName };
                _context.Genres.Add(existingGenre);
            }
            book.Genre = existingGenre;
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(int bookId)
    {
        Book book = await _context.Books.FindAsync(bookId);

        if (book == null)
        {
            return;
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }

    public async Task <IEnumerable<BookDTO>> GetBooksByAuthorNameAsync(string authorName)
    {
        List<BookDTO> books = await _context.Books
            .Where(b => b.Author.Name == authorName)
            .Select(b => new BookDTO 
            {
                Id = b.Id,
                Title = b.Title,
                Summary = b.Summary,
                 AuthorName = b.Author.Name,
                GenreName = b.Genre.GenreName
            })
            .ToListAsync();
        return books;
    }

    public async Task <IEnumerable<BookDTO>> GetBooksByGenreNameAsync(string genreName)
    {
        List<BookDTO> books = await _context.Books
            .Include(b=>b.Author)
            .Include(b=>b.Genre)
            .Where(b => b.Genre.GenreName == genreName)
            .Select(b => new BookDTO 
            {
                Id = b.Id,
                Title = b.Title,
                Summary = b.Summary,
                AuthorName = b.Author.Name,
                GenreName = b.Genre.GenreName
            })
            .ToListAsync();
        return books;
    }
}

