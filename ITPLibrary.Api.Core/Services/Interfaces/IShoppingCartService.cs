namespace ITPLibrary.Api.Core.Services.Interfaces
{
    public interface IShoppingCartService
    {
        public Task<bool> PostBookInCart(int userId, int bookId);
    }
}
