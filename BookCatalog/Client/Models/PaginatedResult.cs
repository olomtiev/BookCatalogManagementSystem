﻿namespace BookCatalog.Client.Models
{
    public class PaginatedResult<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<T> Data { get; set; }
    }
}
