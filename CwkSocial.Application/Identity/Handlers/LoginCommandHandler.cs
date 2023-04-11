using Cwk.Domain.Aggregates.UserProfileAggregate;
using CwkSocial.Api.Services;
using CwkSocial.Application.Enums;
using CwkSocial.Application.Identity.Commands;
using CwkSocial.Application.Models;
using CwkSocial.Dal;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace CwkSocial.Application.Identity.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult<string>>
    {
        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityService _identityService;

        public LoginCommandHandler(DataContext ctx, UserManager<IdentityUser> userManager,
             IdentityService identityService)
        {
            _ctx = ctx;
            _userManager = userManager;
            _identityService = identityService;
        }   
        public async Task<OperationResult<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<string>();
            try
            {

                var identityUser = await ValidateAndGetIdentityAsync(request, result);
                if (identityUser is null)
                {
                    return result;
                }
                var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.IdentityId == identityUser.Id);
                

                result.Payload = GetJwtString(identityUser,userProfile);
                return result;
            }
            catch (Exception ex)
            {
                result.AddUnknownError(ex.Message);
            }
            return result;
        }
        private async Task<IdentityUser>ValidateAndGetIdentityAsync(LoginCommand request, OperationResult<string> result)
        {
            var identityUser = await _userManager.FindByEmailAsync(request.Username);
            if (result.isError)
                result.AddError(ErrorCode.IdentityUserNotExists,IdentityErrorMessages.IdentityUserNotExistsMessage);
                
            
            var validPassword = await _userManager.CheckPasswordAsync(identityUser, request.Password);
            if (!validPassword)
            {
                if (result.isError)
                    result.AddError(ErrorCode.IncorrectUsernameOrPassword, IdentityErrorMessages.IncorrectUsernameOrPasswordMessage);
            }
            return identityUser;
        }
        private string GetJwtString(IdentityUser identityUser, UserProfile userProfile)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
             {
                    new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
                    new Claim("IdentityId", identityUser.Id),
                    new Claim("UserProfileId", userProfile.UserProfileId.ToString())
             });


            var token = _identityService.CreateSecurityToken(claimsIdentity);
            return _identityService.WriteToken(token);
        }
       }
}
