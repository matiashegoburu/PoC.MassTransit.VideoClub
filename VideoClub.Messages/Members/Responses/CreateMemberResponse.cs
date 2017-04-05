using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Messages.Members.Responses
{
    public interface ICreateMemberResponse : Response<bool>
    {

    }

    public class CreateMemberResponse : ResponseMessage<bool>, ICreateMemberResponse
    {
    }
}
