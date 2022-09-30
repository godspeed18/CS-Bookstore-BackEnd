namespace ITPLibrary.Domain.Entites
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public List<Order> Orders { get; set; }
        public List<ShoppingCart> ShoppingCartItems { get; set; }
        public List<RecoveryCode> RecoveryCodes { get; set; }
    }
}
