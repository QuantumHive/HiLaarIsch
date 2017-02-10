using Owin;
using SimpleInjector;

namespace HiLaarIsch
{
    public static class SimpleInjectorExtensions
    {
        public static void Use<TMiddleWare>(this IAppBuilder app, Container container)
            where TMiddleWare : class, IMiddleWare
        {
            app.Use(async (context, next) =>
            {
                await container.GetInstance<TMiddleWare>().Invoke(context, next);
            });
        }
    }
}