using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Domain.Entities
{
    public class UserLogin<TKey> : IdentityUserLogin<TKey> where TKey : IEquatable<TKey>
    {
        #region Overridden Properties
        [StringLength(100)]
        public override string LoginProvider { get; set; }

        [StringLength(100)]
        public override string ProviderKey { get; set; }

        [StringLength(256)]
        public override string ProviderDisplayName { get; set; }

        #endregion

        #region Custom Properties
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(nameof(ApplicationUserLoginId))]
        public int ApplicationUserLoginId { get; set; }
        #endregion
    }
}
