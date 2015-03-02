namespace SidBy.Sklad.Web.Models
{
    using SidBy.Sklad.Web.Resources.Models.Account;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class RegisterExternalLoginModel
    {
        public string ExternalLoginData { get; set; }

        [Display(Name="UserName", ResourceType=typeof(DisplayNameStrings)), Required(ErrorMessageResourceName="Required", ErrorMessageResourceType=typeof(ValidationStrings))]
        public string UserName { get; set; }
    }
}

