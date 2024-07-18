using BookApi.Data.Repository;
using BookApi.Entities;
using BookApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _repository;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookRepository repository, ILogger<BookController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string sortBy = "Id")
        {
            _logger.LogInformation("Fetching books.");

            var booksQuery = _repository.GetBooksAsync();

            // Apply sorting
            booksQuery = SortBooks(booksQuery, sortBy);

            // Apply pagination
            var books = await PaginatedList<Book>.CreateAsync(booksQuery, page, pageSize);

            return Ok(books);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> GetBook(int id)
        {
            _logger.LogInformation($"Fetching book with id: {id}");

            var book = await _repository.GetBookByIdAsync(id);
            if (book == null)
            {
                _logger.LogWarning($"Book with id {id} not found.");
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateBook(Book book)
        {
            await _repository.CreateBookAsync(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> UpdateBook(int id, Book updatedBook)
        {
            var book = await _repository.GetBookByIdAsync(id);
            if (book == null) return NotFound();

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Genre = updatedBook.Genre;
            book.PublicationYear = updatedBook.PublicationYear;

            await _repository.UpdateBookAsync(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteBook(int id) {
            await _repository.DeleteBookAsync(id);
            return NoContent();
        }

        private IQueryable<Book> SortBooks(IQueryable<Book> booksQuery, string sortBy)
        {
            switch (sortBy.ToLower())
            {
                case "title":
                    return booksQuery.OrderBy(b => b.Title);
                case "author":
                    return booksQuery.OrderBy(b => b.Author);
                case "genre":
                    return booksQuery.OrderBy(b => b.Genre);
                case "publicationyear":
                    return booksQuery.OrderBy(b => b.PublicationYear);
                default:
                    return booksQuery.OrderBy(b => b.Id);
            }
        }
    }

    


}
