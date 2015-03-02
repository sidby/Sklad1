namespace SidBy.Sklad.Web.Models
{
    using SidBy.Sklad.Web.Resources.Models.Account;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class LoginModel
    {
        [Display(Name="Password", ResourceType=typeof(DisplayNameStrings)), DataType(DataType.Password), Required(ErrorMessageResourceName="Required", ErrorMessageResourceType=typeof(ValidationStrings))]
        public string Password { get; set; }

        [Display(Name="RememberMe", ResourceType=typeof(DisplayNameStrings))]
        public bool RememberMe { get; set; }

        [Display(Name="UserName", ResourceType=typeof(DisplayNameStrings)), Required(ErrorMessageResourceName="Required", ErrorMessageResourceType=typeof(ValidationStrings))]
        public string UserName { get; set; }
    }
}

