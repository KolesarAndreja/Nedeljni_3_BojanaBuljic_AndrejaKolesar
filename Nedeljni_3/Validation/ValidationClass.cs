using System.Collections.Generic;
using System.Linq;
using Nedeljni_3.Model;

namespace Nedeljni_3.Validation
{
    /// <summary>
    /// Validation class for validating user's inputs
    /// </summary>
    class ValidationClass
    {   
        /// <summary>
        /// Checks if the password is correct
        /// </summary>
        /// <param name="pass">the password we are checking</param>  
        /// <returns>true if correct password, false if not</returns>
        public bool PasswordChecker(string pass)
        {
            if (pass.Length >= 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
