namespace SidBy.Sklad.Web.Resources.Views.Account
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode]
    public class _ExternalLoginsListPartialStrings
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal _ExternalLoginsListPartialStrings()
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

        public static string LogInUsingAnotherService
        {
            get
            {
                return ResourceManager.GetString("LogInUsingAnotherService", resourceCulture);
            }
        }

        public static string LogInUsingYourAccount
        {
            get
            {
                return ResourceManager.GetString("LogInUsingYourAccount", resourceCulture);
            }
        }

        public static string NoExternalAuthenticationServicesConfigured
        {
            get
            {
                return ResourceManager.GetString("NoExternalAuthenticationServicesConfigured", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("SidBy.Sklad.Web.Resources.Views.Account._ExternalLoginsListPartialStrings", typeof(_ExternalLoginsListPartialStrings).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}

