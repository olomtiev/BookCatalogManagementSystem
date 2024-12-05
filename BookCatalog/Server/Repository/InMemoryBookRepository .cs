
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCatalog.Shared;

namespace BookCatalog.Repository
{
    public class InMemoryBookRepository : IBookRepository
    {
        private readonly List<Book> _books = new List<Book>();
        private int _nextId = 1;

        public Task<IEnumerable<Book>> GetBooksAsync()
        {
            return Task.FromResult((IEnumerable<Book>)_books);
        }

        public Task<Book> GetBookByIdAsync(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            return Task.FromResult(book);
        }

        public Task AddBookAsync(Book book)
        {
            book.Id = _nextId++;
            _books.Add(book);
            return Task.CompletedTask;
        }
        public Task AddBooksAsync(List<Book> books)
        {
            foreach (var book in books)
            {
                book.Id = _nextId++;
                _books.Add(book);
            }
            return Task.CompletedTask;
        }

        public Task UpdateBookAsync(Book book)
        {
            var index = _books.FindIndex(b => b.Id == book.Id);
            if (index != -1)
            {
                _books[index] = book;
            }
            return Task.CompletedTask;
        }

        public Task DeleteBookAsync(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _books.Remove(book);
            }
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Book>> SearchBooksAsync(string title, string author, string genre)
        {
            var result = _books.AsEnumerable();
            if (!string.IsNullOrEmpty(title))
                result = result.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(author))
                result = result.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(genre))
                result = result.Where(b => b.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(result);
        }
    }
}
