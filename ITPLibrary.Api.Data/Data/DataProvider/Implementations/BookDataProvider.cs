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
                            Author = reader["Author"].ToString(),
                            Title = reader["Title"].ToString(),
                            Price = (int)reader["Price"],
                            Id = (int)reader["Id"],
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
