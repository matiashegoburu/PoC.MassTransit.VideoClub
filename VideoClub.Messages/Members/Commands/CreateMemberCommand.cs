using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Messages.Members.Commands
{
    public interface ICreateMemberCommand
    {
        string Name { get; set; }
    }

    public class CreateMemberCommand : ICreateMemberCommand
    {
        public string Name { get; set; }
    }
}
