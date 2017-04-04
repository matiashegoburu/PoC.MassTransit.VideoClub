using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using MassTransit;
using MassTransit.UnityIntegration;
using VideoClub.Messages.Titles;
using VideoClub.Consumers.Titles;
using System.Data.SQLite;
using System.Data;
using System.IO;
using AutoMapper;
using PoC.MassTransit.VideoClub.Models;
using VideoClub.Entities;
using VideoClub.Messages.Rentals.Commands;

namespace PoC.MassTransit.VideoClub.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {           
            container.RegisterType<IConsumer<CreateTitleCommand>, TitleConsumer>();

            CreateBus(container);
            SetupAutoMapper(container);
        }

        private static void CreateBus(IUnityContainer container)
        {            
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {   
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.UseJsonSerializer();
            });

            container.RegisterInstance<IBus>(busControl);
            container.RegisterInstance<IBusControl>(busControl);
        }

        private static void SetupAutoMapper(IUnityContainer container)
        {
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<TitleModel, CreateTitleCommand>();
                c.CreateMap<TitleEntity, TitleModel>();

                c.CreateMap<RentalModel, CreateRentalCommand>();
            });

            container.RegisterInstance(config.CreateMapper());
        }
    }

}
