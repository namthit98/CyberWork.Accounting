using System.Text.Json;

namespace API.Extensions;

 public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, int pageNumber,
            int pageSize, int totalCount, int totalPages)
        {
            var paginationHeader = new
            {
                pageNumber,
                pageSize,
                totalCount,
                totalPages
            };
            response.Headers.Add("Pagination", 
                JsonSerializer.Serialize(paginationHeader));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }