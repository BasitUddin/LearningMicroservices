using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Domain.Entities
{
    public class Users : IdentityUser<Guid>
    {
        #region Overridden Properties  
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserId", TypeName = "uniqueidentifier")]
        public override Guid Id { get; set; }

        [StringLength(100)]
        public override string ConcurrencyStamp { get; set; }

        [StringLength(128)]
        public override string? PasswordHash { get; set; }

        [StringLength(128)]
        public override string SecurityStamp { get; set; }
        #endregion

        #region Custom Properties

        [Column(nameof(FullName))]
        public string? FullName { get; set; }

        [Column(nameof(CreatedBy), TypeName = "uniqueidentifier")]
        public Guid CreatedBy { get; set; }

        [Column(nameof(CreatedDate), TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [Column(nameof(ModifiedBy), TypeName = "uniqueidentifier")]
        public Guid ModifiedBy { get; set; }

        [Column(nameof(ModifiedDate), TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }

        [Column(nameof(Active))]
        public bool Active { get; set; }

        [Column(nameof(Deleted))]
        public bool Deleted { get; set; }

        //TODO: It has to be decomposed
        public DateTime? TokenExpiry { get; set; }
        public string? RefreshToken { get; set; }
        #endregion
    }
}
