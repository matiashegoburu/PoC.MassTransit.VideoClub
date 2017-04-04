﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Entities
{
    public class RentalEntity
    {
        public int Id { get; set; }
        public int TitleId { get; set; }
        public int MemberId { get; set; }
        public DateTime FromDate { get; set; }
    }
}
