using System.ComponentModel.DataAnnotations.Schema;

namespace ITPLibrary.Api.Data.Entities
{
    public class RecoveryCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }  
        [ForeignKey("FK_RecoveryCode_User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
