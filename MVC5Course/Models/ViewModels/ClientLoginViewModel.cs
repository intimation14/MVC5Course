using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    public class ClientLoginViewModel
    {
        [Required]
        [StringLength(10, ErrorMessage = "{0} 最大不得超過10個字元")] //商業邏輯驗證
        [DisplayName("姓")]  //自定義顯示名稱
        public string FirstName { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "{0} 最大不得超過{1}個字元")]
        [DisplayName("中間名")]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "{0} 最大不得超過{1}個字元")]
        [DisplayName("名")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("[MF]", ErrorMessage = "{0} 欄位只能輸M或F")]
        [DisplayName("性別")]
        public string Gender { get; set; }
        [Required]
        [DisplayName("生日")]
        [DisplayFormat(DataFormatString = "{0:yyyy/mm/dd}", ApplyFormatInEditMode = true)]    //DateOfBirth 在編輯的時候也套用日期格式
        public Nullable<System.DateTime> DateOfBirth { get; set; }

    }
}