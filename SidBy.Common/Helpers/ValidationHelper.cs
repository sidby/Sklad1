using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SidBy.Common.Helpers
{
    public static class ValidationHelper
    {
        /// <summary>
        /// Validate the "Email" field for email validity
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;

            // validate the "Email" field for email validity
            string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                     @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                     @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(emailRegex);
            if (re.IsMatch(email))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Validate the "Url" field for URL validity
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public static bool IsValidUrl(string link)
        {
            if (String.IsNullOrEmpty(link))
                return false;

            // validate the "Url" field for URL validity
            string urlegex = "^(https?://)"
                    + "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" //user@ 
                    + @"(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP- 199.194.52.184 
                    + "|" // allows either IP or domain 
                    + @"([0-9a-z_!~*'()-]+\.)*" // tertiary domain(s)- www. 
                    + @"([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]\." // second level domain 
                    + "[a-z]{2,6})" // first level domain- .com or .museum 
                    + "(:[0-9]{1,4})?" // port number- :80 
                    + "((/?)|" // a slash isn't required if there is no file name 
                    + "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";
            Regex re = new Regex(urlegex);
            if (re.IsMatch(link))
                return true;
            else
                return false;
        }

    }
}
