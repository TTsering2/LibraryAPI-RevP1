using System.Linq;
using System.Threading.Tasks;
using Libraries.Models;
using Libraries.DTOs;
using Microsoft.EntityFrameworkCore;

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
                    AuthorName = b.Author.Name
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

        public async Task UpdateBooksAsync(int bookId, UpdateBookDTO bookDto)
        {
            Book book = await _context.Books.FindAsync(bookId);
            if (book == null)
            {
                return;
            }

            book.Title = bookDto.Title;
            book.Summary = bookDto.Summary;

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
    }

