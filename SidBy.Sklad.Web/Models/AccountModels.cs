using SidBy.Sklad.Web.Resources.Models.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Globalization;
using System.Web.Security;
using SidBy.Common.DataAnnotations;

namespace SidBy.Sklad.Web.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required(ErrorMessageResourceName="Required", ErrorMessageResourceType=typeof(ValidationStrings))]
        [Display(Name = "UserName", ResourceType = typeof(DisplayNameStrings))]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationStrings))]
        [DataType(DataType.Password)]
        [Display(Name = "OldPassword", ResourceType = typeof(DisplayNameStrings))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationStrings))]
        [StringLength(100, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ValidationStrings), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(DisplayNameStrings))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmNewPassword", ResourceType = typeof(DisplayNameStrings))]
        [Compare("NewPassword", ErrorMessageResourceName = "ComparePasswords", ErrorMessageResourceType = typeof(ValidationStrings))]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationStrings))]
        [Display(Name = "UserName", ResourceType = typeof(DisplayNameStrings))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationStrings))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(DisplayNameStrings))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(DisplayNameStrings))]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationStrings))]
        [Display(Name = "UserName", ResourceType = typeof(DisplayNameStrings))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationStrings))]
        [StringLength(100, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ValidationStrings), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(DisplayNameStrings))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(DisplayNameStrings))]
        [Compare("Password", ErrorMessageResourceName = "ComparePasswords", ErrorMessageResourceType = typeof(ValidationStrings))]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationStrings))]
        [Display(Name = "UserEmail", ResourceType = typeof(DisplayNameStrings))]
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceName = "EmailAddress", ErrorMessageResourceType = typeof(ValidationStrings))]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationStrings))]
        [Display(Name = "Surname", ResourceType = typeof(DisplayNameStrings))]
        public string Surname { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationStrings))]
        [Display(Name = "Name", ResourceType = typeof(DisplayNameStrings))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationStrings))]
        [Display(Name = "MiddleName", ResourceType = typeof(DisplayNameStrings))]
        public string MiddleName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationStrings))]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone", ResourceType = typeof(DisplayNameStrings))]
        [Phone(ErrorMessage = null, ErrorMessageResourceName = "Phone", ErrorMessageResourceType = typeof(ValidationStrings))]
        public string Phone { get; set; }

        //[Required(AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description", ResourceType = typeof(DisplayNameStrings))]
        public string Description { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
