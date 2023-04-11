using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkSocial.Application.Identity
{
    public class IdentityErrorMessages
    {
        public const string IdentityUserNotExistsMessage = "Unable to find user with the given username!";
        public const string IncorrectUsernameOrPasswordMessage = "Username and/or Password does not match!";
        public const string IdentityUserAlreadyExistsMessage = "Provided email address already exists!";
    }
}
