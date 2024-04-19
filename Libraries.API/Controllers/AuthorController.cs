using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Libraries.Services;
using Libraries.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authors.API.Controllers

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    // GET: api/author
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
    {
        try
        {
            List<Author> authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving authors: {ex.Message}");
        }
    }

    // GET: api/author/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Author>> GetAuthorById(int id)
    {
        try
        {
            Author author = await _authorService.GetAnAuthorAsync(id);
            if (author == null)
            {
                return NotFound($"Author with ID {id} not found.");
            }
            return Ok(author);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving author: {ex.Message}");
        }
    }

    // POST: api/author
    [HttpPost]
    public async Task<ActionResult<Author>> CreateAuthor([FromBody] Author author)
    {
        try
        {
            Author createdAuthor = await _authorService.CreateAnAuthorAsync(author);
            return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.Id }, createdAuthor);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating author: {ex.Message}");
        }
    }

    // PATCH: api/author/{id}
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateAuthor(int authorId, Author updatedAuthor)
    {
        try
        {
            await _authorService.UpdateAnAuthorAsync(authorId, updatedAuthor);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating author: {ex.Message}");
        }
    }

    // DELETE: api/author/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        try
        {
            await _authorService.DeleteAnAuthorAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting author: {ex.Message}");
        }
    }
}

