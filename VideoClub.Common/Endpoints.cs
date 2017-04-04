using System;

namespace VideoClub.Common
{
    public static class Endpoints
    {
        public static Uri Titulos { get; }
        public static Uri Rentals { get; }

        static Endpoints()
        {
            Titulos = new Uri("rabbitmq://localhost/titulos_queue");
            Rentals = new Uri("rabbitmq://localhost/rentals_queue");
        }
    }
}
