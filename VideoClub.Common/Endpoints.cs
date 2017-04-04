using System;

namespace VideoClub.Common
{
    public static class Endpoints
    {
        public static Uri Titles { get; }

        static Endpoints()
        {
            Titles = new Uri("rabbitmq://localhost/titles_queue");
        }
    }
}
