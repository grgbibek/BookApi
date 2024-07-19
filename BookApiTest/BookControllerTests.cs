using BookApi.Controllers;
using BookApi.Data.Repository;
using BookApi.Entities;
using BookApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;


namespace BookApi.Tests
{
    [TestFixture]
    public class BookControllerTests
    {
        private Mock<IBookRepository> _mockRepository;
        private Mock<ILogger<BookController>> _mockLogger;
        private BookController _controller;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IBookRepository>();
            _mockLogger = new Mock<ILogger<BookController>>();
            _controller = new BookController(_mockRepository.Object, _mockLogger.Object);
        }

        [Test]
        public async Task GetBooks_ReturnsPaginatedListOfBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1", Genre = "Genre 1", PublicationYear = 2001 },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2", Genre = "Genre 2", PublicationYear = 2002 },
                new Book { Id = 3, Title = "Book 3", Author = "Author 3", Genre = "Genre 3", PublicationYear = 2003 },
            };

            _mockRepository.Setup(repo => repo.GetBooksAsync(1,2, "PublicationYear")).ReturnsAsync(books);

            // Act
            var result = await _controller.GetBooks(1, 2, "PublicationYear");

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<List<Book>>(okResult.Value);
            var returnedBooks = okResult.Value as List<Book>;
            Assert.AreEqual(books.Count(), returnedBooks.Count());
        }

        [Test]
        public async Task GetBook_ReturnsBook_WhenBookExists()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Book 1", Author = "Author 1", Genre = "Genre 1", PublicationYear = 2001 };
            _mockRepository.Setup(repo => repo.GetBookByIdAsync(1)).ReturnsAsync(book);

            // Act
            var result = await _controller.GetBook(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(book, okResult.Value);
        }

        [Test]
        public async Task GetBook_ReturnsNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetBookByIdAsync(1)).ReturnsAsync((Book)null);

            // Act
            var result = await _controller.GetBook(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task CreateBook_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "New Book", Author = "New Author", Genre = "New Genre", PublicationYear = 2024 };
            _mockRepository.Setup(repo => repo.CreateBookAsync(book)).ReturnsAsync(new Book { Id = 1, Title = "Existing Book", Author = "Existing Author", Genre = "Existing Genre", PublicationYear = 2020 });

            // Act
            var result = await _controller.CreateBook(book);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.AreEqual(nameof(_controller.GetBook), createdAtActionResult.ActionName);
            Assert.AreEqual(book.Id, createdAtActionResult.RouteValues["id"]);
            Assert.AreEqual(book, createdAtActionResult.Value);
        }

        [Test]
        public async Task UpdateBook_ReturnsNoContent_WhenBookExists()
        {
            // Arrange
            var existingBook = new Book { Id = 1, Title = "Existing Book", Author = "Existing Author", Genre = "Existing Genre", PublicationYear = 2020 };
            var updatedBook = new Book { Title = "Updated Book", Author = "Updated Author", Genre = "Updated Genre", PublicationYear = 2021 };

            _mockRepository.Setup(repo => repo.GetBookByIdAsync(1)).ReturnsAsync(existingBook);
            _mockRepository.Setup(repo => repo.UpdateBookAsync(It.IsAny<Book>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateBook(1, updatedBook);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            _mockRepository.Verify(repo => repo.UpdateBookAsync(It.Is<Book>(b => b.Title == updatedBook.Title && b.Author == updatedBook.Author && b.Genre == updatedBook.Genre && b.PublicationYear == updatedBook.PublicationYear)), Times.Once);
        }

        [Test]
        public async Task UpdateBook_ReturnsNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            var updatedBook = new Book { Title = "Updated Book", Author = "Updated Author", Genre = "Updated Genre", PublicationYear = 2021 };
            _mockRepository.Setup(repo => repo.GetBookByIdAsync(1)).ReturnsAsync((Book)null);

            // Act
            var result = await _controller.UpdateBook(1, updatedBook);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task DeleteBook_ReturnsNoContent()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteBookAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteBook(1);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            _mockRepository.Verify(repo => repo.DeleteBookAsync(1), Times.Once);
        }
    }
}
