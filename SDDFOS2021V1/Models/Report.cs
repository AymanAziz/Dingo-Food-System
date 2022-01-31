//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SDDFOS2021V1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Report
    {
        public int ReportID { get; set; }
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int AdminID { get; set; }
        public int PaymentID { get; set; }
        public Nullable<System.DateTime> ReportDate { get; set; }
    
        public virtual Admin Admin { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual OrderDetail OrderDetail { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
