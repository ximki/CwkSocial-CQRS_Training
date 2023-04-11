using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cwk.Domain.Aggregates.UserProfileAggregate;
using Cwk.Domain.Exceptions;
using Cwk.Domain.Validators.PostValidators;

namespace Cwk.Domain.Aggregates.PostAggregate
{
    public class Post
    {
        private readonly List<PostComment> _comments=new List<PostComment>();
        private readonly List<PostInteraction> _interactions = new List<PostInteraction>();
        private Post()
        {
           
        }
        public Guid PostId { get; private set; }
        public Guid UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public string TextContent { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime LastModified { get; private set; }
        public IEnumerable<PostComment>Comments{ get { return _comments; } }
        public IEnumerable<PostInteraction> Interactions { get { return _interactions; } }

        //pblic methods
        //factories
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userProfileId">User Profile ID</param>
        /// <param name="textContent">Post content</param>
        /// <returns cref="PostNotValidException"></returns>
        public static Post CreatePost(Guid userProfileId, string textContent)
        {
            var Validators = new PostValidator();
            var objectToValidate= new Post
            {
                UserProfileId = userProfileId,
                TextContent = textContent,
                DateCreated = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
           
            };
            var validationResult = Validators.Validate(objectToValidate);
            if (validationResult.IsValid) return objectToValidate;

            var exception = new PostNotValidException("Post is not valid");
            validationResult.Errors.ForEach(ve => exception.ValidationErrors.Add(ve.ErrorMessage));
            throw exception;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newText">The updatet post text</param>
        /// <exception cref="PostNotValidException"></exception>
        public void UpdatePostText(string newText)
        {
            if (string.IsNullOrWhiteSpace(newText))
            {
                var exception = new PostNotValidException("Can not update post. Post text is not valid");
                exception.ValidationErrors.Add("The provided text is either null or a whitespace");
                throw exception;
            }
            TextContent = newText;
            LastModified = DateTime.UtcNow;
        }
        public void AddPostComment(PostComment newComment)
        {
            _comments.Add(newComment);
        }
        public void RemovePostComment(PostComment toRemove)
        {
            _comments.Remove(toRemove);
        }

        public void AddInteraction(PostInteraction newInteraction)
        {
            _interactions.Add(newInteraction);
        }
        public void RemoveInteraction(PostInteraction toRemove)
        {
            _interactions.Remove(toRemove);
        }
    }
}
