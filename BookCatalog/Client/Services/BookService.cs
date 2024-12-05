using System.Net.Http.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BookCatalog.Shared;
using Microsoft.AspNetCore.Components.Forms;
using static System.Net.WebRequestMethods;
using BookCatalog.Client.Models;

namespace BookCatalog.Client.Services
{
    public class BookService
    {
        private readonly HttpClient _httpClient;
        private readonly string baseUrl = "http://localhost:7138/api";
        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
         
        public async Task<PaginatedResult<Book>?> SearchBooksAsync(string? title, string? author, string? genre, int page = 1, int pageSize = 10)
        {
            var query = $"?title={title}&author={author}&genre={genre}&page={page}&pageSize={pageSize}";
            var responce = await _httpClient.GetFromJsonAsync<PaginatedResult<Book>>($"{baseUrl}/books/search{query}");
            return responce;
        }

        public async Task<Book> GetBookAsync(int id)
        {
            var responce = await _httpClient.GetFromJsonAsync<Book>($"{baseUrl}/books/{id}");
            return responce;
        }

        public async Task AddBookAsync(Book book)
        {
            await _httpClient.PostAsJsonAsync($"{baseUrl}/books", book);
        }

        public async Task UpdateBookAsync(Book book)
        {
            await _httpClient.PutAsJsonAsync($"{baseUrl}/books/{book.Id}", book);
        }

        public async Task DeleteBookAsync(int id)
        {
            await _httpClient.DeleteAsync($"{baseUrl}/books/{id}");
        }
        public async Task<bool> HandleFileUpload(InputFileChangeEventArgs e)
        {
            var file = e.File;
            if (file != null)
            {
                var content = new MultipartFormDataContent
                {
                    { new StreamContent(file.OpenReadStream()), "file", file.Name }
                };

                var response = await _httpClient.PostAsync($"{baseUrl}/books/import", content);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                return true;

            }
            return false;
        }

    }
}
