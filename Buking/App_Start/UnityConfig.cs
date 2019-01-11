using Buking.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Data.Entity;
using System.Web;
using Unity;
using Unity.Injection;

namespace Buking
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<DbContext, DataContext>();

            container.RegisterType<IUserStore<IdentityUser>, UserStore<IdentityUser>>();

            container.RegisterType<UserManager<IdentityUser>>();

            container.RegisterType<UserManager<IdentityUser, string>>(
                new InjectionFactory(o => o.Resolve<UserManager<IdentityUser>>()));

            container.RegisterType<IAuthenticationManager>(
                new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));

            container.RegisterType<SignInManager<IdentityUser, string>>();

            container.RegisterType<IRoleStore<IdentityRole, string>, RoleStore<IdentityRole>>();

            container.RegisterType<RoleManager<IdentityRole>>();
        }
    }
}