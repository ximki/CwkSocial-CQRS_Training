using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkSocial.Application.Enums
{
    public enum ErrorCode
    {
        NotFound=404,
        ServerError=500,

        //Validation Error Codes (100-199)
        ValidationError=101,

        //Insfrastructure Error codes (200-299)
        IdentityUserAlreadyExists =201,
        IdentityCreationFailed = 202,
        IdentityUserNotExists = 203,
        IncorrectUsernameOrPassword=204,
        UserProfileNotExists=205,

        //Application Error Codes (300-399)
        UserProfileNotMatchForAction=300,

        UnknownError =999
       

    }
}
