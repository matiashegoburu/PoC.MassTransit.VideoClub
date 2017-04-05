using AutoMapper;
using MassTransit;
using Microsoft.Practices.Unity;
using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;
using VideoClub.Consumers.Titles;
using VideoClub.Entities;
using VideoClub.Messages.Titles;
using VideoClub.Messages.Rentals.Commands;
using VideoClub.Consumers.Rentals;
using VideoClub.Consumers.Members;
using VideoClub.Messages.Members.Commands;

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
            _container.RegisterType<TitleConsumer>();

            _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("titles_queue", ec =>
                {
                    ec.Consumer(() => _container.Resolve<TitleConsumer>());
                });

                cfg.ReceiveEndpoint("rentals_queue", ec =>
                {
                    ec.Consumer(() => _container.Resolve<RentalsConsumer>());
                });

                cfg.ReceiveEndpoint("members_queue", ec =>
                {
                    ec.Consumer(() => _container.Resolve<MembersConsumer>());
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
                CreateDatabase(_cnn as SQLiteConnection);
            }

            _container.RegisterInstance<IDbConnection>(_cnn);
        }

        private static void SetupAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateTitleMessage, TitleEntity>();
                cfg.CreateMap<ICreateRentalCommand, RentalEntity>();
                cfg.CreateMap<ICreateMemberCommand, MemberEntity>();
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

        private static async void CreateDatabase(SQLiteConnection cnn)
        {
            await cnn.OpenAsync();

            var sql =
                @"CREATE TABLE Titles (
                        ID          INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title      STRING,
                        Description STRING,
                        Category      STRING
                    );";

            using (var command = new SQLiteCommand(sql, cnn as SQLiteConnection))
            {
                command.ExecuteNonQuery();
            }

            cnn.Close();
        }
    }
}
