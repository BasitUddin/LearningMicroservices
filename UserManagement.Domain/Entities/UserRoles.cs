using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Domain.Entities
{
    public class UserRoles<TKey> : IdentityUserRole<TKey> where TKey : IEquatable<TKey>
    {
        #region Overridden Properties

        #endregion

        #region Custom Properties
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(nameof(UserRoleId))]
        public int UserRoleId { get; set; }

        [Column(nameof(CreatedBy), TypeName = "uniqueidentifier")]
        public Guid CreatedBy { get; set; }

        [Column(nameof(CreatedDate), TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        [Column(nameof(ModifiedBy), TypeName = "uniqueidentifier")]
        public Guid ModifiedBy { get; set; }

        [Column(nameof(ModifiedDate), TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        #endregion

    }
}
