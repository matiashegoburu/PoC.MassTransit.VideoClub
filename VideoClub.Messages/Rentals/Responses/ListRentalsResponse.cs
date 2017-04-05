using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entities;

namespace VideoClub.Messages.Rentals.Responses
{
    public interface IListRentalsResponse : Response<List<RentalEntity>> { }

    public class ListRentalsResponse : ResponseMessage<List<RentalEntity>>, IListRentalsResponse { }
}
