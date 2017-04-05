using System.Collections.Generic;
using VideoClub.Entities;

namespace VideoClub.Messages.Rentals.Responses
{
    public interface IListRentalsResponse : Response<List<RentalEntity>> { }

    public class ListRentalsResponse : ResponseMessage<List<RentalEntity>>, IListRentalsResponse { }
}
