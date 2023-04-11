
namespace CwkSocial.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class UserProfilesController:BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public UserProfilesController(IMediator mediator, IMapper mapper)
        {
            _mediator=mediator;
            _mapper=mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            var query = new GetAllUserProfiles();
            var responses = await _mediator.Send(query);
            var profiles = _mapper.Map<List<UserProfileResponse>>(responses.Payload);
            return Ok(profiles);
        }

        [Route(ApiRoutes.UserProfiles.IdRoute)]
        [HttpGet]
        
        public async Task<IActionResult> GetUserProfileById(string id)
        {
            var query = new GetUserProfileById { UserProfileId = Guid.Parse(id) };
            var response = await _mediator.Send(query);
            if (response.isError)
            {
                return HandleErrorResponse(response.Errors);
            }
            var userProfile= _mapper.Map<UserProfileResponse>(response.Payload);
            return Ok(userProfile);
        }

        [HttpPatch]
        [Route(ApiRoutes.UserProfiles.IdRoute)]
        [ValidateModel]
        [ValidateGuid(ApiRoutes.UserProfiles.IdRoute)]
        public async Task<IActionResult>UpdateUserProfile(string id,UserProfileCreateUpdate updatedProfile)
        {
            var command = _mapper.Map<UpdateUserProfileBasicInfo>(updatedProfile);
            command.UserProfileId = Guid.Parse(id);
            var response=await _mediator.Send(command);
            if (response.isError)
            {
              return  HandleErrorResponse(response.Errors);
            }
            return NoContent();
        }

        [HttpDelete]
        [Route(ApiRoutes.UserProfiles.IdRoute)]
        [ValidateGuid(ApiRoutes.UserProfiles.IdRoute)]
        public async Task<IActionResult> DeleteUserProfile(string id)
        {
            var command = new DeleteUserProfile() { UserProfileId=Guid.Parse(id)};
            var response = await _mediator.Send(command);
            if (response.isError)
            {
                return HandleErrorResponse(response.Errors);
            }
            return NoContent();

        }
    }
}
