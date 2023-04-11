namespace CwkSocial.Api.MappingProfiles
{
    public class PostMapping :Profile
    {
        public PostMapping()
        {
            CreateMap<Post, PostResponse>();
            CreateMap<PostComment, PostCommentResponse>();
        }
    }
}
