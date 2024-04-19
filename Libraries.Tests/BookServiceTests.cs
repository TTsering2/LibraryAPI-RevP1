using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Libraries.Services;
using Libraries.Data;
using Libraries.DTOs;

public class BookServiceTests
{
    private readonly Mock<IBooksRepository> _mockRepo;
    private readonly BookService _service;

    public BookServiceTests()
    {
        _mockRepo = new Mock<IBooksRepository>();
        _service = new BookService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetBookByIdAsync_ReturnsBook_WhenBookExists()
    {
        // Arrange
        var bookId = 1;
        var expectedBook = new BookDTO { Id = bookId, Title = "Test Book" };
        _mockRepo.Setup(x => x.GetBookByIdAsync(bookId)).ReturnsAsync(expectedBook);

        // Act
        var result = await _service.GetBookByIdAsync(bookId);

        // Assert
        Assert.Equal(expectedBook, result);
        _mockRepo.Verify(x => x.GetBookByIdAsync(bookId), Times.Once);
    }

    [Fact]
    public async Task GetAllBooksAsync_ReturnsAllBooks()
    {
        // Arrange
        var expectedBooks = new List<BookDTO> { new BookDTO { Id = 1, Title = "Book 1" } };
        _mockRepo.Setup(x => x.GetAllBooksAsync()).ReturnsAsync(expectedBooks);

        // Act
        var result = await _service.GetAllBooksAsync();

        // Assert
        Assert.Equal(expectedBooks, result);
        _mockRepo.Verify(x => x.GetAllBooksAsync(), Times.Once);
    }

    [Fact]
    public async Task CreateBookAsync_CreatesAndReturnsBook()
    {
        // Arrange
        var bookDto = new BookCreateDTO { Title = "New Book" };
        var createdBook = new BookDTO { Id = 1, Title = "New Book" };
        _mockRepo.Setup(x => x.CreateBookAsync(bookDto)).ReturnsAsync(createdBook);

        // Act
        var result = await _service.CreateBookAsync(bookDto);

        // Assert
        Assert.Equal(createdBook, result);
        _mockRepo.Verify(x => x.CreateBookAsync(bookDto), Times.Once);
    }

    [Fact]
    public async Task UpdateBooksAsync_UpdatesBook()
    {
        // Arrange
        var bookId = 1;
        var updateDto = new UpdateBookDTO { Title = "Updated Title" };
        _mockRepo.Setup(x => x.UpdateBooksAsync(bookId, updateDto)).Returns(Task.CompletedTask);

        // Act
        await _service.UpdateBooksAsync(bookId, updateDto);

        // Assert
        _mockRepo.Verify(x => x.UpdateBooksAsync(bookId, updateDto), Times.Once);
    }

    [Fact]
    public async Task DeleteBookAsync_DeletesBook()
    {
        // Arrange
        var bookId = 1;
        _mockRepo.Setup(x => x.DeleteBookAsync(bookId)).Returns(Task.CompletedTask);

        // Act
        await _service.DeleteBookAsync(bookId);

        // Assert
        _mockRepo.Verify(x => x.DeleteBookAsync(bookId), Times.Once);
    }

    [Fact]
    public async Task GetBooksByAuthorNameAsync_ReturnsBooks()
    {
        // Arrange
        var authorName = "Author";
        var expectedBooks = new List<BookDTO> { new BookDTO { Id = 1, Title = "Book 1" } };
        _mockRepo.Setup(x => x.GetBooksByAuthorNameAsync(authorName)).ReturnsAsync(expectedBooks);

        // Act
        var result = await _service.GetBooksByAuthorNameAsync(authorName);

        // Assert
        Assert.Equal(expectedBooks, result);
        _mockRepo.Verify(x => x.GetBooksByAuthorNameAsync(authorName), Times.Once);
    }

    [Fact]
    public async Task GetBooksByGenreNameAsync_ReturnsBooks()
    {
        // Arrange
        var genreName = "Genre";
        var expectedBooks = new List<BookDTO> { new BookDTO { Id = 1, Title = "Book 1" } };
        _mockRepo.Setup(x => x.GetBooksByGenreNameAsync(genreName)).ReturnsAsync(expectedBooks);

        // Act
        var result = await _service.GetBooksByGenreNameAsync(genreName);

        // Assert
        Assert.Equal(expectedBooks, result);
        _mockRepo.Verify(x => x.GetBooksByGenreNameAsync(genreName), Times.Once);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(100)]
    public async Task GetBookByIdAsync_ReturnsBook_WhenBookExists2(int bookId)
    {
        // Arrange
        var expectedBook = new BookDTO { Id = bookId, Title = $"Test Book {bookId}" };
        _mockRepo.Setup(x => x.GetBookByIdAsync(bookId)).ReturnsAsync(expectedBook);

        // Act
        var result = await _service.GetBookByIdAsync(bookId);

        // Assert
        Assert.Equal(expectedBook, result);
        _mockRepo.Verify(x => x.GetBookByIdAsync(bookId), Times.Once);
    }

    [Theory]
    [InlineData("Author")]
    [InlineData("Another Author")]
    [InlineData("Unknown")]
    public async Task GetBooksByAuthorNameAsync_ReturnsBooks2(string authorName)
    {
        // Arrange
        var expectedBooks = new List<BookDTO> 
        { 
            new BookDTO { Id = 1, Title = $"Book by {authorName}" } 
        };
        _mockRepo.Setup(x => x.GetBooksByAuthorNameAsync(authorName)).ReturnsAsync(expectedBooks);

        // Act
        var result = await _service.GetBooksByAuthorNameAsync(authorName);

        // Assert
        Assert.Equal(expectedBooks, result);
        _mockRepo.Verify(x => x.GetBooksByAuthorNameAsync(authorName), Times.Once);
    }

}
