
namespace CwkSocial.Api.Extensions
{
    public static class RegisterExtensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder, Type scanningType)
        {
            var registars = GetRegistrars<IWebApplicationBuilderRegistar>(scanningType);
            foreach (var registrar in registars)
            {
                registrar.RegisterServices(builder);
            }
        }
        public static void RegisterPipelineComponents(this WebApplication app, Type scanningType)
        {
            var registars = GetRegistrars<IWebApplicationRegistar>(scanningType);
            foreach (var registrar in registars)
            {
                registrar.RegisterPipelineComponents(app);
            }

        }

        private static IEnumerable<T> GetRegistrars<T>(Type scanningType) where T:IRegistrar
        {
            return scanningType.Assembly.GetTypes()
                .Where(t => t.IsAssignableTo(typeof(T)) && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<T>();
        }
    }
}
