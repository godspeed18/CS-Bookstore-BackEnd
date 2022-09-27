namespace ITPLibrary.Domain.Entites
{
    public class RecoveryCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }  
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
