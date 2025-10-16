using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Entities
{
    public class RoleClaims<TKey> : IdentityRoleClaim<TKey> where TKey : IEquatable<TKey>
    {
        #region Overridden Properties
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("RoleClaimId")]
    public override int Id { get; set; }

    [StringLength(100)]
    public override string ClaimType { get; set; }

    [StringLength(256)]
    public override string ClaimValue { get; set; }
    #endregion

    #region Custom PropertiesRequired]
    [Column(nameof(CreatedBy), TypeName = "uniqueidentifier")]
    public Guid CreatedBy { get; set; }

    [Required]
    [Column(nameof(CreatedDate), TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Required]
    [Column(nameof(ModifiedBy), TypeName = "uniqueidentifier")]
    public Guid ModifiedBy { get; set; }

    [Column(nameof(ModifiedDate), TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }
    #endregion
}
}
