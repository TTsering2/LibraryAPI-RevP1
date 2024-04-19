using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Libraries.Models;
using Libraries.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Genres.API.Controllers

[Route("api/[controller]")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenreController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    // GET: api/genre
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genre>>> GetAllGenres()
    {
        try
        {
            var genres = await _genreService.GetAllGenresAsync();
            return Ok(genres);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving genres: {ex.Message}");
        }
    }

    // GET: api/genre/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Genre>> GetGenreById(int id)
    {
        try
        {
            var genre = await _genreService.GetAGenreAsync(id);
            if (genre == null)
            {
                return NotFound($"Genre with ID {id} not found.");
            }
            return Ok(genre);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving genre: {ex.Message}");
        }
    }

    // POST: api/genre
    [HttpPost]
    public async Task<ActionResult<Genre>> CreateGenre([FromBody] Genre genre)
    {
        try
        {
            Genre createdGenre = await _genreService.CreateGenreAsync(genre);
            return CreatedAtAction(nameof(GetGenreById), new { id = createdGenre.Id }, createdGenre);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating genre: {ex.Message}");
        }
    }

    // PATCH: api/genre/{id}
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateGenre(int genreId, Genre updatedGenre)
    {
        try
        {
            await _genreService.UpdateAGenreAsync(genreId, updatedGenre);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating genre: {ex.Message}");
        }
    }

    // DELETE: api/genre/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        try
        {
            await _genreService.DeleteAGenreAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting genre: {ex.Message}");
        }
    }
}

