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
                new Book { Id = 1, Title = "The Enigma of the East", Author = "Isabella May", Genre = "Mystery", PublicationYear = 1995 },
                new Book { Id = 2, Title = "Ocean's Heartbeat", Author = "Liam Smith", Genre = "Romance", PublicationYear = 2005 },
                new Book { Id = 3, Title = "Journey to the Unknown", Author = "Emma Brown", Genre = "Adventure", PublicationYear = 2010 },
                new Book { Id = 4, Title = "The Lost Kingdom", Author = "Oliver Johnson", Genre = "Fantasy", PublicationYear = 1998 },
                new Book { Id = 5, Title = "Whispers in the Wind", Author = "Sophia Davis", Genre = "Thriller", PublicationYear = 2002 },
                new Book { Id = 6, Title = "Echoes of the Past", Author = "Ethan Miller", Genre = "Historical", PublicationYear = 2015 },
                new Book { Id = 7, Title = "Shadows of Time", Author = "Mia Wilson", Genre = "Sci-Fi", PublicationYear = 2008 },
                new Book { Id = 8, Title = "Rising Sun", Author = "James Lee", Genre = "Drama", PublicationYear = 2001 },
                new Book { Id = 9, Title = "Frozen Dreams", Author = "Charlotte Taylor", Genre = "Fantasy", PublicationYear = 2012 },
                new Book { Id = 10, Title = "Desert Mirage", Author = "Henry White", Genre = "Adventure", PublicationYear = 2003 },
                new Book { Id = 11, Title = "Starlit Night", Author = "Amelia Thomas", Genre = "Romance", PublicationYear = 2009 },
                new Book { Id = 12, Title = "The Hidden Realm", Author = "Noah Walker", Genre = "Fantasy", PublicationYear = 2018 },
                new Book { Id = 13, Title = "Eternal Flame", Author = "Grace Martinez", Genre = "Drama", PublicationYear = 1999 },
                new Book { Id = 14, Title = "Wild Frontier", Author = "Benjamin Brown", Genre = "Historical", PublicationYear = 2006 },
                new Book { Id = 15, Title = "Silver Shadows", Author = "Zoe Scott", Genre = "Thriller", PublicationYear = 2013 },
                new Book { Id = 16, Title = "Storm Chaser", Author = "Daniel Moore", Genre = "Sci-Fi", PublicationYear = 2004 },
                new Book { Id = 17, Title = "Lunar Eclipse", Author = "Ava Harris", Genre = "Fantasy", PublicationYear = 2000 },
                new Book { Id = 18, Title = "Golden Sands", Author = "Jackson Clark", Genre = "Adventure", PublicationYear = 2007 },
                new Book { Id = 19, Title = "The Eternal Horizon", Author = "Olivia Rodriguez", Genre = "Romance", PublicationYear = 2011 },
                new Book { Id = 20, Title = "Whispering Woods", Author = "Lucas Martinez", Genre = "Mystery", PublicationYear = 2014 }
            };

            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}

