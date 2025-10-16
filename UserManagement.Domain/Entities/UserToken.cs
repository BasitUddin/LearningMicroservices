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
    public class UserToken<TKey> : IdentityUserToken<TKey> where TKey : IEquatable<TKey>
    {
        #region Overridden Properties

        [StringLength(100)]
        public override string LoginProvider { get; set; }

        [StringLength(100)]
        public override string Name { get; set; }

        [StringLength(256)]
        public override string Value { get; set; }
        #endregion

        #region Custom Properties
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(nameof(ApplicationUserTokenId))]
        public int ApplicationUserTokenId { get; set; }
        #endregion
    }
}
