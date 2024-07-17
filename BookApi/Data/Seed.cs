using BookApi.Entities;
using BookApi.Helpers;

namespace BookApi.Data
{
    public static class Seed
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            context.Users.Add(new User
            {
                Email = "admin",
                PasswordHash = PasswordHasher.HashPassword("admin"),
                Role ="Admin",
            });

            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1", Genre = "Genre 1", PublicationYear = 2001 },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2", Genre = "Genre 2", PublicationYear = 2002 },
                new Book { Id = 3, Title = "Book 3", Author = "Author 3", Genre = "Genre 3", PublicationYear = 2003 },
            };

            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}

