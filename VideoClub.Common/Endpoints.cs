using System;

namespace VideoClub.Common
{
    public static class Endpoints
    {
        public static Uri Titles { get; }
        public static Uri Rentals { get; }

        static Endpoints()
        {
            Titles = new Uri("rabbitmq://localhost/titles_queue");
            Rentals = new Uri("rabbitmq://localhost/rentals_queue");
        }
    }
}
