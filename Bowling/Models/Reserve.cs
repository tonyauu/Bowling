using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bowling.Models
{
    public class Reserve
    {
        public int ReserveID { get; set; }
        public int PersonID { get; set; }

        public int? LaneID { get; set; }
        public virtual Lane lanes { get; set; }
        //public virtual ICollection<Lane> Lanes { get; set; }

    }
}