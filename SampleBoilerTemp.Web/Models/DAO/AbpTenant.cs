//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SampleBoilerTemp.Web.Models.DAO
{
    using System;
    using System.Collections.Generic;
    
    public partial class AbpTenant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AbpTenant()
        {
            this.StationaryRequests = new HashSet<StationaryRequest>();
        }
    
        public int Id { get; set; }
        public Nullable<int> EditionId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string TenancyName { get; set; }
        public string ConnectionString { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<long> DeleterUserId { get; set; }
        public Nullable<System.DateTime> DeletionTime { get; set; }
        public Nullable<System.DateTime> LastModificationTime { get; set; }
        public Nullable<long> LastModifierUserId { get; set; }
        public System.DateTime CreationTime { get; set; }
        public Nullable<long> CreatorUserId { get; set; }
    
        public virtual AbpEdition AbpEdition { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StationaryRequest> StationaryRequests { get; set; }
        public virtual AbpUser AbpUser { get; set; }
        public virtual AbpUser AbpUser1 { get; set; }
        public virtual AbpUser AbpUser2 { get; set; }
    }
}