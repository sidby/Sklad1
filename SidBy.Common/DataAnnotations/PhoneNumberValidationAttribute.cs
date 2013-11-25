using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneNumbers;

namespace SidBy.Common.DataAnnotations
{
    /// <summary>
    /// Use PhoneAttribute instead
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class PhoneNumberValidationAttribute : DataTypeAttribute
    {
        public PhoneNumberValidationAttribute()
            : base(DataType.PhoneNumber)
        { }

        public override bool IsValid(object value)
        {
            string str = Convert.ToString(value, CultureInfo.CurrentCulture);
            
            if (string.IsNullOrEmpty(str))
                return true;
   
            return PhoneNumberUtil.IsViablePhoneNumber(str);
        }

    }
}
