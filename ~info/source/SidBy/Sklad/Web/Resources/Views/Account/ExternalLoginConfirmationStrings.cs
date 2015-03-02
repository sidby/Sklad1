namespace SidBy.Sklad.Web.Resources.Views.Account
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [DebuggerNonUserCode, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), CompilerGenerated]
    public class ExternalLoginConfirmationStrings
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal ExternalLoginConfirmationStrings()
        {
        }

        public static string AssociateYourAccount
        {
            get
            {
                return ResourceManager.GetString("AssociateYourAccount", resourceCulture);
            }
        }

        public static string AssociationForm
        {
            get
            {
                return ResourceManager.GetString("AssociationForm", resourceCulture);
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

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("SidBy.Sklad.Web.Resources.Views.Account.ExternalLoginConfirmationStrings", typeof(ExternalLoginConfirmationStrings).Assembly);
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

        public static string YouHaveSuccessfullyAuthenticated
        {
            get
            {
                return ResourceManager.GetString("YouHaveSuccessfullyAuthenticated", resourceCulture);
            }
        }
    }
}

