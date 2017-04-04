using System;

namespace VideoClub.Messages.Rentals.Commands
{
    public interface ICreateRentalCommand
    {
        int MemberId { get; }
        int TitleId { get; }
        DateTime FromDate { get; }
    }

    public class CreateRentalCommand : ICreateRentalCommand
    {
        public DateTime FromDate { get; set; }

        public int MemberId { get; set; }

        public int TitleId { get; set; }
    }
}
