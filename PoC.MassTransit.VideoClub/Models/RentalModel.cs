using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.MassTransit.VideoClub.Models
{
    public class RentalModel
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int TitleId { get; set; }
        public DateTime FromDate { get; set; }
    }
}
