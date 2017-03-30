using AutoMapper;
using MassTransit;
using Microsoft.Practices.Unity;
using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;
using VideoClub.Consumers.Titulos;
using VideoClub.Entities;
using VideoClub.Messages.Titulos;

namespace VideoClub.Server.Console
{
    class Program
    {
        private static IUnityContainer _container;
        private static IBusControl _bus;
        private static IDbConnection _cnn;

        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            SetupUnity();
            SetupDb();
            SetupAutoMapper();

            _cnn.Open();
            await _bus.StartAsync();

            System.Console.WriteLine("Press any key to close the bus...");
            System.Console.ReadKey();

            _cnn.Close();
            await _bus.StopAsync();            
        }

        private static void SetupUnity()
        {
            _container = new UnityContainer();

            // register each consumer
            _container.RegisterType<TituloConsumer>();

            _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("titulos_queue", ec =>
                {
                    ec.Consumer(() => _container.Resolve<TituloConsumer>());
                });
            });

            _container.RegisterInstance<IBus>(_bus);
        }

        private static void SetupDb()
        {
            // DB Should not be here, but on the MT console app/windows service that handles the messages!!
            var dbNeedsInitialize = false;
            if (!File.Exists(DbFile))
            {
                File.Create(DbFile);
                dbNeedsInitialize = true;
            }

            _cnn = SimpleDbConnection();
            if (dbNeedsInitialize)
            {
                CreateDatabase(_cnn);
            }

            _container.RegisterInstance<IDbConnection>(_cnn);
        }

        private static void SetupAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateTituloMessage, TituloEntity>();
            });

            _container.RegisterInstance(config.CreateMapper());
        }


        private static string DbFile
        {
            get { return Environment.CurrentDirectory + "\\SimpleDb.sqlite"; }
        }

        private static SQLiteConnection SimpleDbConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }

        private static void CreateDatabase(IDbConnection cnn)
        {
            using (var command = cnn.CreateCommand())
            {
                command.CommandText =
                    @"CREATE TABLE Titulos (
                        ID          INTEGER PRIMARY KEY AUTOINCREMENT,
                        Titulo      STRING,
                        Descripcion STRING,
                        Genero      STRING
                    );";
                command.ExecuteNonQuery();
            }
        }
    }
}
