namespace VideoClub.Messages.Rentals.Responses
{
    public interface ICreateRentalResponse : Response<bool>
    {

    }

    public class CreateRentalResponse : ResponseMessage<bool>, ICreateRentalResponse
    {
    }
}
