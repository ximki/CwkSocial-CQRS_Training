namespace CwkSocial.Api.Registars
{
    public interface IWebApplicationBuilderRegistar:IRegistrar
    {
        void RegisterServices(WebApplicationBuilder builder);
    }
}
