namespace CwkSocial.Api.Registars
{
    public interface IWebApplicationRegistar:IRegistrar
    {
        public void RegisterPipelineComponents(WebApplication app);
    }
}
