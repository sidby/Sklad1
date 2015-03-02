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
    public class _RemoveExternalLoginsPartialStrings
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal _RemoveExternalLoginsPartialStrings()
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

        public static string RegisteredExternalLogins
        {
            get
            {
                return ResourceManager.GetString("RegisteredExternalLogins", resourceCulture);
            }
        }

        public static string Remove
        {
            get
            {
                return ResourceManager.GetString("Remove", resourceCulture);
            }
        }

        public static string RemoveCredentialFromYourAccount
        {
            get
            {
                return ResourceManager.GetString("RemoveCredentialFromYourAccount", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("SidBy.Sklad.Web.Resources.Views.Account._RemoveExternalLoginsPartialStrings", typeof(_RemoveExternalLoginsPartialStrings).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}

