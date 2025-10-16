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
    public class Roles : IdentityRole<Guid>
    {
        public Roles(string name)
           : base(name)
        {
            this.Id = Guid.NewGuid();
        }

        #region Overridden Properties
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("RoleId", TypeName = "uniqueidentifier")]
        public override Guid Id { get; set; }

        [StringLength(100)]
        public override string Name { get; set; }

        [StringLength(100)]
        public override string NormalizedName { get; set; }

        [StringLength(100)]
        public override string ConcurrencyStamp { get; set; }
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

        [Column(nameof(isSuperAdminRole), TypeName = "bit")]
        public bool isSuperAdminRole { get; set; }

        [Column(nameof(isAdminRole), TypeName = "bit")]
        public bool isAdminRole { get; set; }

        [Column(nameof(isManagerRole), TypeName = "bit")]
        public bool isManagerRole { get; set; }

        [Column(nameof(isUserRole), TypeName = "bit")]
        public bool isUserRole { get; set; }


        [Column(nameof(isAnonymousRole), TypeName = "bit")]
        public bool isAnonymousRole { get; set; }


        [Column(nameof(isActive), TypeName = "bit")]
        public bool isActive { get; set; }
        #endregion
    }
}
