﻿namespace CwkSocial.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class IdentityController:BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public IdentityController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpPost]
        [Route(ApiRoutes.Identity.Registration)]
        [ValidateModel]
        public async Task<IActionResult> Register(UserRegistration registration)
        {
            var command=_mapper.Map<RegisterIdentity>(registration);
            var result = await _mediator.Send(command);

            if (result.isError)
            {
               return HandleErrorResponse(result.Errors);
            }
            var authenticationResult = new AuthenticationResult() { Token = result.Payload };
            return Ok(authenticationResult);
        }

        [HttpPost]
        [Route(ApiRoutes.Identity.Login)]
        [ValidateModel]
        public async Task<IActionResult>LogIn(Login logIn)
        {
            var command = _mapper.Map<LoginCommand>(logIn);
            var result = await _mediator.Send(command);
            if (result.isError)
            {
                return HandleErrorResponse(result.Errors);
            }
            var authenticationResult=new AuthenticationResult() { Token=result.Payload };

            return Ok(authenticationResult);
        }
    }
}
