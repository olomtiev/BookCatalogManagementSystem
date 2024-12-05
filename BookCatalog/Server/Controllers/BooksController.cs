
using BookCatalog.Shared;
using BookCatalog.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsvHelper;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string? title, [FromQuery] string? author, [FromQuery] string? genre, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var books = await _bookRepository.SearchBooksAsync(title, author, genre);

            var totalBooks = books.Count();
            var totalPages = (int)Math.Ceiling(totalBooks / (double)pageSize);

            if (page > totalPages && totalBooks > 0)
            {
                return BadRequest($"Page {page} does not exist.");
            }
            var paginatedBooks = books
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var response = new
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = totalBooks,
                TotalPages = totalPages,
                Data = paginatedBooks
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            await _bookRepository.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (book == null || book.Id != id)
            {
                return BadRequest();
            }
            var existingBook = await _bookRepository.GetBookByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }
            await _bookRepository.UpdateBookAsync(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var existingBook = await _bookRepository.GetBookByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }
            await _bookRepository.DeleteBookAsync(id);
            return NoContent();
        }
        
        [HttpPost("import")]
        public async Task<IActionResult> ImportBooks(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }
            using var reader = new StreamReader(file.OpenReadStream());
            var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            var books = csvReader.GetRecords<Book>().ToList();

            await _bookRepository.AddBooksAsync(books);
            return Ok();
        }

    }
}
