using System;

namespace VideoClub.Common
{
    public static class Endpoints
    {
        public static Uri Titulos { get; }

        static Endpoints()
        {
            Titulos = new Uri("rabbitmq://localhost/titulos_queue");
        }
    }
}
