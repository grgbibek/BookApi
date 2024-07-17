using BookApi.Entities;

namespace BookApi.Data.Repository
{
    public interface IBookRepository
    {
        IQueryable<Book> GetBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<Book> CreateBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }

}
