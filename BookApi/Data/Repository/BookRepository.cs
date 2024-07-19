using BookApi.Entities;
using BookApi.Helpers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Data.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetBooksAsync(int page, int pageSize, string sortBy)
        {
            var booksQuery = _context.Books.AsQueryable();
            return await PaginatedList<Book>.CreateAsync(booksQuery, page, pageSize);
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
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
