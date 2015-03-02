namespace SidBy.Sklad.Web.Resources.Views.Shared
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode]
    public class _LayoutStrings
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal _LayoutStrings()
        {
        }

        public static string About
        {
            get
            {
                return ResourceManager.GetString("About", resourceCulture);
            }
        }

        public static string ApplicationName
        {
            get
            {
                return ResourceManager.GetString("ApplicationName", resourceCulture);
            }
        }

        public static string Contact
        {
            get
            {
                return ResourceManager.GetString("Contact", resourceCulture);
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

        public static string Home
        {
            get
            {
                return ResourceManager.GetString("Home", resourceCulture);
            }
        }

        public static string Lang
        {
            get
            {
                return ResourceManager.GetString("Lang", resourceCulture);
            }
        }

        public static string Logo
        {
            get
            {
                return ResourceManager.GetString("Logo", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("SidBy.Sklad.Web.Resources.Views.Shared._LayoutStrings", typeof(_LayoutStrings).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}

