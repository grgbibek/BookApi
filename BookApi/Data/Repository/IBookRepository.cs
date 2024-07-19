using BookApi.Entities;

namespace BookApi.Data.Repository
{
    public interface IBookRepository
    {
        Task<List<Book>> GetBooksAsync(int page, int pageSize, string sortBy);
        Task<Book> GetBookByIdAsync(int id);
        Task<Book> CreateBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }

}
