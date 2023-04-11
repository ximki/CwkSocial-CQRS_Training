
namespace CwkSocial.Api.Registars
{
    public class IdentityRegistrar : IWebApplicationBuilderRegistar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var jwtSettings = new JwtSettings();
            builder.Configuration.Bind(nameof(JwtSettings),jwtSettings);

            var jwtSection = builder.Configuration.GetSection(nameof(JwtSettings));
            builder.Services.Configure<JwtSettings>(jwtSection);


            builder.Services.AddAuthentication(a=>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt=>
            {

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters() //setting how we want to validate token 
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SigningKey)),
                    ValidateIssuer = true,
                    ValidIssuer=jwtSettings.Issuer,
                    ValidateAudience=true,
                    ValidAudiences=jwtSettings.Audiences,
                    RequireExpirationTime=false,
                    ValidateLifetime=true
                };
                jwt.Audience = jwtSettings.Audiences[0];
                jwt.ClaimsIssuer = jwtSettings.Issuer;
            });
        }
    }
}
