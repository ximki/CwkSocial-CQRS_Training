using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwk.Domain.Exceptions
{
    public class UserProfileNotValidException:NotValidException
    {
        public UserProfileNotValidException() { }
        public UserProfileNotValidException(string message):base(message){ }
        public UserProfileNotValidException(string message,Exception inner) : base(message,inner) { }

    }
}
