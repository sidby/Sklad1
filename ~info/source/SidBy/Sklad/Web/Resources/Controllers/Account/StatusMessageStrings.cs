namespace SidBy.Sklad.Web.Resources.Controllers.Account
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode]
    public class StatusMessageStrings
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal StatusMessageStrings()
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

        public static string ExternalLoginWasRemoved
        {
            get
            {
                return ResourceManager.GetString("ExternalLoginWasRemoved", resourceCulture);
            }
        }

        public static string PasswordChanged
        {
            get
            {
                return ResourceManager.GetString("PasswordChanged", resourceCulture);
            }
        }

        public static string PasswordSet
        {
            get
            {
                return ResourceManager.GetString("PasswordSet", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("SidBy.Sklad.Web.Resources.Controllers.Account.StatusMessageStrings", typeof(StatusMessageStrings).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}

