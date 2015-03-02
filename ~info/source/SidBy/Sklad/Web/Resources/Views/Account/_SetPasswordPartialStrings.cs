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
    public class _SetPasswordPartialStrings
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal _SetPasswordPartialStrings()
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

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("SidBy.Sklad.Web.Resources.Views.Account._SetPasswordPartialStrings", typeof(_SetPasswordPartialStrings).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        public static string SetPassword
        {
            get
            {
                return ResourceManager.GetString("SetPassword", resourceCulture);
            }
        }

        public static string SetPasswordForm
        {
            get
            {
                return ResourceManager.GetString("SetPasswordForm", resourceCulture);
            }
        }

        public static string YouDoNotHaveLocalPasswordForThisSite
        {
            get
            {
                return ResourceManager.GetString("YouDoNotHaveLocalPasswordForThisSite", resourceCulture);
            }
        }
    }
}

