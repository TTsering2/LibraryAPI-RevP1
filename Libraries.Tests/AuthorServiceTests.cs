using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Libraries.Models;
using Libraries.Services;
using Libraries.Data;

public class AuthorServiceTests
{
    private readonly Mock<IAuthorsRepository> _mockRepo;
    private readonly AuthorService _service;

    public AuthorServiceTests()
    {
        _mockRepo = new Mock<IAuthorsRepository>();
        _service = new AuthorService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllAuthorsAsync_ReturnsAllAuthors()
    {
        // Arrange
        var expectedAuthors = new List<Author>
        {
            new Author { Id = 1, Name = "Author One" },
            new Author { Id = 2, Name = "Author Two" }
        };
        _mockRepo.Setup(repo => repo.GetAllAuthorsAsync()).ReturnsAsync(expectedAuthors);

        // Act
        var result = await _service.GetAllAuthorsAsync();

        // Assert
        Assert.Equal(expectedAuthors, result);
        _mockRepo.Verify(repo => repo.GetAllAuthorsAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAnAuthorAsync_ReturnsAuthor_WhenAuthorExists()
    {
        // Arrange
        int authorId = 1;
        var expectedAuthor = new Author { Id = authorId, Name = "Author One" };
        _mockRepo.Setup(repo => repo.GetAnAuthorAsync(authorId)).ReturnsAsync(expectedAuthor);

        // Act
        var result = await _service.GetAnAuthorAsync(authorId);

        // Assert
        Assert.Equal(expectedAuthor, result);
        _mockRepo.Verify(repo => repo.GetAnAuthorAsync(authorId), Times.Once);
    }

    [Fact]
    public async Task DeleteAnAuthorAsync_DeletesAuthor_WhenAuthorExists()
    {
        // Arrange
        int authorId = 1;

        // Act
        await _service.DeleteAnAuthorAsync(authorId);

        // Assert
        _mockRepo.Verify(repo => repo.DeleteAnAuthorAsync(authorId), Times.Once);
    }
}
