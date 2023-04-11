
namespace CwkSocial.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public PostsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var result = await _mediator.Send(new GetAllPosts());
            var mapped = _mapper.Map<List<PostResponse>>(result.Payload);
            if (result.isError)
            {
                HandleErrorResponse(result.Errors);
            }

            return Ok();
        }
        [HttpGet]
        [Route(ApiRoutes.Posts.IdRoute)]
        [ValidateGuid("id")]
        public async Task<IActionResult> GetById(string id)
        {
            var postId = Guid.Parse(id);
            var query = new GetPostById() { PostId = postId };
            var result = await _mediator.Send(query);
            var mapped = _mapper.Map<List<PostResponse>>(result.Payload);
            if (result.isError)
            {
                HandleErrorResponse(result.Errors);
            }

            return Ok(mapped);
        }
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreatePost([FromBody] PostCreate newPost)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();
            var command = new CreatePostCommand()
            {
                UserProfileId = userProfileId,
                TextContent = newPost.TextContent
            };
            var result = await _mediator.Send(command);
            var mapped = _mapper.Map<PostResponse>(result.Payload);
            if (result.isError)
            {
                HandleErrorResponse(result.Errors);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Payload.UserProfileId }, mapped);
        }
        [HttpPatch]
        [Route(ApiRoutes.Posts.IdRoute)]
        [ValidateGuid("id")]
        [ValidateModel]
        public async Task<IActionResult> UpdatePostText([FromBody] PostUpdate updatedPost, string id)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();
            var command = new UpdatePostText()
            {
                NewText = updatedPost.Text,
                PostId = Guid.Parse(id),
                UserProfileId=userProfileId

            };
            var result = await _mediator.Send(command);
            if (result.isError)
            {
                HandleErrorResponse(result.Errors);
            }
            return NoContent();
        }
        [HttpDelete]
        [Route(ApiRoutes.Posts.IdRoute)]
        [ValidateGuid("id")]
        public async Task<IActionResult> DeletePost(string id)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();
            var command = new DeletePost() { PostId = Guid.Parse(id) , UserProfileId=userProfileId};
            var result = await _mediator.Send(command);
            if (result.isError)
            {
                HandleErrorResponse(result.Errors);
            }
            return NoContent();
        }
        [HttpGet]
        [Route(ApiRoutes.Posts.PostComments)]
        [ValidateGuid("postId")]
        public async Task<IActionResult> GetCommentsByPostId(string postId)
        {
            var query = new GetPostComments() { PostId = Guid.Parse(postId) };
            var result = await _mediator.Send(query);

            if (result.isError)
            {
                HandleErrorResponse(result.Errors);
            }
            var comments = _mapper.Map<List<PostCommentResponse>>(result.Payload);
            return Ok(comments);
        }
        [HttpPost]
        [Route(ApiRoutes.Posts.PostComments)]
        [ValidateGuid("postId")]
        [ValidateModel]
        public async Task<IActionResult> AddCommentToPost(string postId, [FromBody] PostCommentCreate comment)
        {
            var isValidGuid = Guid.TryParse(comment.UserProfileId, out var userProfileId);
            if (!isValidGuid)
            {
                var apiError = new ErrorResponse()
                {
                    StatusCode = 400,
                    StatusPhrase = "Bad Request",
                    Timestamp = DateTime.Now,

                };
                apiError.Errors.Add($"Provide Guid{ comment.UserProfileId} for userProfilrId is not a valid Guid");
            }
            var command = new AddPostCommentCommand()
            {
                PostId = Guid.Parse(postId),
                CommentText = comment.Text,
                UserProfileId = userProfileId
            };
            var result = await _mediator.Send(command);
            if (result.isError)
            {
                HandleErrorResponse(result.Errors);
            }
            var newComment = _mapper.Map<PostCommentResponse>(result.Payload);
            return Ok(newComment);
        }

    }
}
