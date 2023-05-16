using ITSupportSystem.Core1.Contracts;
using ITSupportSystem.Core1.Models;
using ITSupportSystem.Core1.ViewModel;
using ITSupportSystem.DataAccess.SQL;
using ITSupportSystem.Services;
using System;
using Unity;

namespace ITSupportSystem.WebUI
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
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();


            // container.RegisterType<IRepository<Users>, SQLRepository<Users>>();

            container.RegisterType<ILoginRepository, LoginRepository>();
            container.RegisterType<ILoginService, LoginServices>();

            container.RegisterType<IRepository<Role>, SQLRepository<Role>>();
            container.RegisterType<IRoleServices, RoleServices>();
            container.RegisterType<IRoleRepository, RoleRepository>();

            container.RegisterType<IUserServices, UserServices>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IRepository<UserRole>, SQLRepository<UserRole>>();

            container.RegisterType<IRepository<CommonLookUp>, SQLRepository<CommonLookUp>>();
            container.RegisterType<ICommonLookUpServices, CommonLookUpServices>();

            container.RegisterType<IFormRepository, FormRepository>();
            container.RegisterType<IFormServices, FormServices>();

            container.RegisterType<IPermissionRepository, PermissionRepository>();
            container.RegisterType<IPermissionServices, PermissionServices>();

            container.RegisterType<ITicketRepository, TicketRepository>();
            container.RegisterType<ITicketServices, TicketServices>();
            container.RegisterType<IRepository<TicketAttachment>, SQLRepository<TicketAttachment>>();

            container.RegisterType<IRepository<TicketComment>, SQLRepository<TicketComment>>();
        }
    }
}