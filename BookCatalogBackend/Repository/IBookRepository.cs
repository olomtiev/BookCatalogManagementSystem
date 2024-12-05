
using BookCatalogBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BookCatalogBackend.Repository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task<IEnumerable<Book>> SearchBooksAsync(string title, string author, string genre);
    }
}

