using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bowling.Models
{

    public class Lane
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LaneID { get; set; }
        public string Name{ get; set; }
        public int NumbOfPeople { get; set; }
        
        public virtual ICollection<Reserve> Reserves { get; set; }
        
    }
}