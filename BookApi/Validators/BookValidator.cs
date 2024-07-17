using BookApi.Entities;
using FluentValidation;

namespace BookApi.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(b => b.Title).NotEmpty();
            RuleFor(b => b.Author).NotEmpty();
            RuleFor(b => b.Genre).NotEmpty();
            RuleFor(b => b.PublicationYear).GreaterThan(0);
        }
    }
}
