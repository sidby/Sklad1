namespace SidBy.Sklad.Web.Resources.Models.Account
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode]
    public class ValidationStrings
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal ValidationStrings()
        {
        }

        public static string ComparePasswords
        {
            get
            {
                return ResourceManager.GetString("ComparePasswords", resourceCulture);
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

        public static string EmailAddress
        {
            get
            {
                return ResourceManager.GetString("EmailAddress", resourceCulture);
            }
        }

        public static string Phone
        {
            get
            {
                return ResourceManager.GetString("Phone", resourceCulture);
            }
        }

        public static string Required
        {
            get
            {
                return ResourceManager.GetString("Required", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("SidBy.Sklad.Web.Resources.Models.Account.ValidationStrings", typeof(ValidationStrings).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        public static string StringLength
        {
            get
            {
                return ResourceManager.GetString("StringLength", resourceCulture);
            }
        }
    }
}

