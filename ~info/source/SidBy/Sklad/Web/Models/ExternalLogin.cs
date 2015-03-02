namespace SidBy.Sklad.Web.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class ExternalLogin
    {
        public string Provider { get; set; }

        public string ProviderDisplayName { get; set; }

        public string ProviderUserId { get; set; }
    }
}

