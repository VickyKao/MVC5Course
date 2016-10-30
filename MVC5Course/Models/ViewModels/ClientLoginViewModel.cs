using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels {
    public class ClientLoginViewModel {
        [Required]
        [StringLength(10, ErrorMessage = "{0} 最大不可超過 {1} 個字元")]
        [DisplayName("名字")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "{0} 最大不可超過 {1} 個字元")]
        [DisplayName("中間名")]
        [DataType(DataType.Password)]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(5, ErrorMessage = "{0} 最大不可超過 {1} 個字元")]
        [DisplayName("姓")]
        public string LastName { get; set; }
        [DisplayName("生日")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)] //只有chrome
        public Nullable<System.DateTime> DateOfBirth { get; set; }
    }
}