namespace SidBy.Sklad.Web.Resources.Views.Account
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), CompilerGenerated, DebuggerNonUserCode]
    public class RegisterStrings
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal RegisterStrings()
        {
        }

        public static string CreateNewAccount
        {
            get
            {
                return ResourceManager.GetString("CreateNewAccount", resourceCulture);
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

        public static string Register
        {
            get
            {
                return ResourceManager.GetString("Register", resourceCulture);
            }
        }

        public static string RegistrationForm
        {
            get
            {
                return ResourceManager.GetString("RegistrationForm", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("SidBy.Sklad.Web.Resources.Views.Account.RegisterStrings", typeof(RegisterStrings).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        public static string ViewBagTitle
        {
            get
            {
                return ResourceManager.GetString("ViewBagTitle", resourceCulture);
            }
        }
    }
}

