namespace SidBy.Sklad.Web.Resources.Views.Account
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
    public class LoginStrings
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal LoginStrings()
        {
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

        public static string IfYouDoNotHaveAccount
        {
            get
            {
                return ResourceManager.GetString("IfYouDoNotHaveAccount", resourceCulture);
            }
        }

        public static string LogIn
        {
            get
            {
                return ResourceManager.GetString("LogIn", resourceCulture);
            }
        }

        public static string LogInForm
        {
            get
            {
                return ResourceManager.GetString("LogInForm", resourceCulture);
            }
        }

        public static string Register
        {
            get
            {
                return ResourceManager.GetString("Register", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("SidBy.Sklad.Web.Resources.Views.Account.LoginStrings", typeof(LoginStrings).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        public static string UseAnotherServiceToLogIn
        {
            get
            {
                return ResourceManager.GetString("UseAnotherServiceToLogIn", resourceCulture);
            }
        }

        public static string UseLocalAccountToLogIn
        {
            get
            {
                return ResourceManager.GetString("UseLocalAccountToLogIn", resourceCulture);
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

