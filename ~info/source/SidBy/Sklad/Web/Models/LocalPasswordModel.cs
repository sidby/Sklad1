namespace SidBy.Sklad.Web.Models
{
    using SidBy.Sklad.Web.Resources.Models.Account;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class LocalPasswordModel
    {
        [Display(Name="ConfirmNewPassword", ResourceType=typeof(DisplayNameStrings)), Compare("NewPassword", ErrorMessageResourceName="ComparePasswords", ErrorMessageResourceType=typeof(ValidationStrings)), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name="NewPassword", ResourceType=typeof(DisplayNameStrings)), DataType(DataType.Password), Required(ErrorMessageResourceName="Required", ErrorMessageResourceType=typeof(ValidationStrings)), StringLength(100, ErrorMessageResourceName="StringLength", ErrorMessageResourceType=typeof(ValidationStrings), MinimumLength=6)]
        public string NewPassword { get; set; }

        [Display(Name="OldPassword", ResourceType=typeof(DisplayNameStrings)), DataType(DataType.Password), Required(ErrorMessageResourceName="Required", ErrorMessageResourceType=typeof(ValidationStrings))]
        public string OldPassword { get; set; }
    }
}

