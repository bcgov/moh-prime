using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Prime.Models.Api
{
    public class PaginatedResponse<T>
    {
        public List<T> Results { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
        public int PageSize { get; set; }

        public PaginatedResponse(PaginatedList<T> list)
        {
            Results = list;
            Page = list.Page;
            TotalPages = list.TotalPages;
            TotalResults = list.TotalResults;
            PageSize = list.PageSize;
        }
    }
    public class PaginatedList<T> : List<T>
    {
        private const int _pageSize = 100;

        public int Page { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalResults { get; private set; }
        public int PageSize { get; private set; }

        private PaginatedList(IEnumerable<T> items, int totalResults, int page, int pageSize)
        {
            Page = page;
            TotalPages = (int)Math.Ceiling(totalResults / (double)pageSize);
            TotalResults = totalResults;
            PageSize = pageSize;

            AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return Page > 1;
            }
        }

        public bool HasNextPage
        {
            get
            {
                return Page < TotalPages;
            }
        }

        public PaginatedResponse<T> Response
        {
            get => new(this);
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int page)
        {
            var pageNumber = page < 1 ? 1 : page;
            var count = await source.CountAsync();

            var items = await source.Skip((pageNumber - 1) * _pageSize).Take(_pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageNumber, _pageSize);
        }
    }
}
