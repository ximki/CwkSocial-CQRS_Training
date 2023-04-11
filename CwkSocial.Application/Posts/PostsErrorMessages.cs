using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkSocial.Application.Posts
{
    public class PostsErrorMessages
    {
        public const string PostNotFoundMessage = "No post  with ID {0} was found";
        public const string UserProfileNotMatchForActionMessage = "Post {0} are allowed only by post owner";
    }
}
