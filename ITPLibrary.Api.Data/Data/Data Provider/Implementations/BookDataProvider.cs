using ITPLibrary.Api.Data.Data.Data_Provider.Interfaces;
using ITPLibrary.Api.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ITPLibrary.Api.Data.Data.Data_Provider.Implementations
{
    public class BookDataProvider : IBookDataProvider
    {
        private readonly IConfiguration _configuration;

        public BookDataProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Book>> GetPopularBooks()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                        SELECT 
                            Id,
                            Author,
                            Price,
                            Title 
                        FROM
                            Books";

                    SqlDataReader reader = command.ExecuteReader();
                    List<Book> books = new List<Book>();
                    while (await reader.ReadAsync())
                    {
                        var currentBook = new Book()
                        {
                            Author = await reader.GetFieldValueAsync<string>(2),
                            Title = await reader.GetFieldValueAsync<string>(1),
                            Price = await reader.GetFieldValueAsync<int>(3),
                            Id = await reader.GetFieldValueAsync<int>(0),
                        };
                        books.Add(currentBook);
                    }

                    await reader.CloseAsync();
                    return books;
                }
            }
        }
    }
}
