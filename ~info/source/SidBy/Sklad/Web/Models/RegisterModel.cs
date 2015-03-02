namespace SidBy.Sklad.Web.Models
{
    using SidBy.Sklad.Web.Resources.Models.Account;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class RegisterModel
    {
        [Compare("Password", ErrorMessageResourceName="ComparePasswords", ErrorMessageResourceType=typeof(ValidationStrings)), DataType(DataType.Password), Display(Name="ConfirmPassword", ResourceType=typeof(DisplayNameStrings))]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Password), Display(Name="Password", ResourceType=typeof(DisplayNameStrings)), Required(ErrorMessageResourceName="Required", ErrorMessageResourceType=typeof(ValidationStrings)), StringLength(100, ErrorMessageResourceName="StringLength", ErrorMessageResourceType=typeof(ValidationStrings), MinimumLength=6)]
        public string Password { get; set; }

        [Display(Name="UserName", ResourceType=typeof(DisplayNameStrings)), Required(ErrorMessageResourceName="Required", ErrorMessageResourceType=typeof(ValidationStrings))]
        public string UserName { get; set; }
    }
}

