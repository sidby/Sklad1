namespace SidBy.Sklad.Web.Resources.Models.Account
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [DebuggerNonUserCode, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), CompilerGenerated]
    public class DisplayNameStrings
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal DisplayNameStrings()
        {
        }

        public static string ConfirmNewPassword
        {
            get
            {
                return ResourceManager.GetString("ConfirmNewPassword", resourceCulture);
            }
        }

        public static string ConfirmPassword
        {
            get
            {
                return ResourceManager.GetString("ConfirmPassword", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        public static string Description
        {
            get
            {
                return ResourceManager.GetString("Description", resourceCulture);
            }
        }

        public static string MiddleName
        {
            get
            {
                return ResourceManager.GetString("MiddleName", resourceCulture);
            }
        }

        public static string Name
        {
            get
            {
                return ResourceManager.GetString("Name", resourceCulture);
            }
        }

        public static string NewPassword
        {
            get
            {
                return ResourceManager.GetString("NewPassword", resourceCulture);
            }
        }

        public static string OldPassword
        {
            get
            {
                return ResourceManager.GetString("OldPassword", resourceCulture);
            }
        }

        public static string Password
        {
            get
            {
                return ResourceManager.GetString("Password", resourceCulture);
            }
        }

        public static string Phone
        {
            get
            {
                return ResourceManager.GetString("Phone", resourceCulture);
            }
        }

        public static string RememberMe
        {
            get
            {
                return ResourceManager.GetString("RememberMe", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("SidBy.Sklad.Web.Resources.Models.Account.DisplayNameStrings", typeof(DisplayNameStrings).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        public static string Surname
        {
            get
            {
                return ResourceManager.GetString("Surname", resourceCulture);
            }
        }

        public static string UserEmail
        {
            get
            {
                return ResourceManager.GetString("UserEmail", resourceCulture);
            }
        }

        public static string UserName
        {
            get
            {
                return ResourceManager.GetString("UserName", resourceCulture);
            }
        }
    }
}

