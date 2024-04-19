using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Libraries.Models;
using Libraries.Services;
using Libraries.Data;

public class GenreServiceTests
{
    private readonly Mock<IGenresRepository> _mockRepo;
    private readonly GenreService _service;

    public GenreServiceTests()
    {
        _mockRepo = new Mock<IGenresRepository>();
        _service = new GenreService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllGenresAsync_ReturnsAllGenres()
    {
        // Arrange
        var genres = new List<Genre>
        {
            new Genre { Id = 1, GenreName = "Fantasy" },
            new Genre { Id = 2, GenreName = "Science Fiction" }
        };
        _mockRepo.Setup(repo => repo.GetAllGenresAsync()).ReturnsAsync(genres);

        // Act
        var result = await _service.GetAllGenresAsync();

        // Assert
        Assert.Equal(genres.Count, result.Count());  
        Assert.True(genres.SequenceEqual(result));   
    }


    [Fact]
    public async Task GetAGenreAsync_ReturnsGenreById()
    {
        // Arrange
        var genre = new Genre { Id = 1, GenreName = "Fantasy" };
        _mockRepo.Setup(repo => repo.GetAGenreAsync(1)).ReturnsAsync(genre);

        // Act
        var result = await _service.GetAGenreAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(genre.Id, result.Id);
        Assert.Equal(genre.GenreName, result.GenreName);
    }

    [Fact]
    public async Task DeleteAGenreAsync_DeletesGenre()
    {
        // Arrange
        int genreId = 1;

        // Act
        await _service.DeleteAGenreAsync(genreId);

        // Assert
        _mockRepo.Verify(repo => repo.DeleteAGenreAsync(It.IsAny<int>()), Times.Once);
    }

}
