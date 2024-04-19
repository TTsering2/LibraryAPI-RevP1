using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Models;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Data;

public class AuthorsRepository : IAuthorsRepository
{
    private readonly LibrariesDbContext _context;

     public AuthorsRepository(LibrariesDbContext context)
    {
         _context = context;
    }

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        List<Author> authors = await _context.Authors.ToListAsync();
        return authors;
    }

    public async Task<Author> GetAnAuthorAsync(int authorId)
    {
        return await _context.Authors.FindAsync(authorId);
    }

    public async Task UpdateAnAuthorAsync(int authorId, Author updatedAuthor)
    {
        if (updatedAuthor == null)
        {
            throw new ArgumentNullException(nameof(updatedAuthor));
        }

        Author author = await _context.Authors.FindAsync(authorId);

        if (author == null)
        {
            throw new InvalidOperationException($"Author with ID '{authorId}' not found");
        }

        if (!string.IsNullOrWhiteSpace(updatedAuthor.Name))
        {
            author.Name = updatedAuthor.Name;
        }

        _context.Entry(author).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAnAuthorAsync(int authorId)
    {
        Author author = await _context.Authors.FindAsync(authorId);

        if (author == null)
        {
            throw new InvalidOperationException($"Author with ID '{authorId}' not found");
        }

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
    }

    public async Task<Author> CreateAnAuthorAsync(Author newAuthor)
    {
        if (newAuthor == null)
        {
            throw new ArgumentNullException(nameof(newAuthor));
        }

        Author existingAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Name == newAuthor.Name);

        if (existingAuthor != null)
        {
            throw new InvalidOperationException($"Author '{newAuthor.Name}' already exists");
        }

        _context.Authors.Add(newAuthor);
        await _context.SaveChangesAsync();

        return newAuthor;
    }
}

