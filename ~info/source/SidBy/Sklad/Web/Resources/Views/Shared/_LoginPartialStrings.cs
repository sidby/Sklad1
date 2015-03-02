namespace SidBy.Sklad.Web.Resources.Views.Shared
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
    public class _LoginPartialStrings
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal _LoginPartialStrings()
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

        public static string Hello
        {
            get
            {
                return ResourceManager.GetString("Hello", resourceCulture);
            }
        }

        public static string LogIn
        {
            get
            {
                return ResourceManager.GetString("LogIn", resourceCulture);
            }
        }

        public static string LogOff
        {
            get
            {
                return ResourceManager.GetString("LogOff", resourceCulture);
            }
        }

        public static string Manage
        {
            get
            {
                return ResourceManager.GetString("Manage", resourceCulture);
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
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("SidBy.Sklad.Web.Resources.Views.Shared._LoginPartialStrings", typeof(_LoginPartialStrings).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}

