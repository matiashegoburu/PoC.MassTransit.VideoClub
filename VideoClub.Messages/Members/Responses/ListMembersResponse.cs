using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entities;

namespace VideoClub.Messages.Members.Responses
{
    public class ListMembersResponse : ResponseMessage<List<MemberEntity>>, IListMembersResponse { }

    public interface IListMembersResponse : Response<List<MemberEntity>>
    {
    }
}
