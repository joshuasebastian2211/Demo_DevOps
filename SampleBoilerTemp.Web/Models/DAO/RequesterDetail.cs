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
    
    public partial class RequesterDetail
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public string Phone { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string PRNumber { get; set; }
    
        public virtual ClientDetail ClientDetail { get; set; }
        public virtual StationaryRequest StationaryRequest { get; set; }
    }
}