namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(vw_ClientOrderTotalMetaData))]
    public partial class vw_ClientOrderTotal
    {
    }
    
    public partial class vw_ClientOrderTotalMetaData
    {
        [Required]
        public int ClientId { get; set; }
        
        [StringLength(40, ErrorMessage="欄位長度不得大於 40 個字元")]
        public string FirstName { get; set; }
        
        [StringLength(40, ErrorMessage="欄位長度不得大於 40 個字元")]
        public string LastName { get; set; }
        public Nullable<decimal> OrderTotal { get; set; }
    }
}
