using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Entity
{
     public class MapPositionEntity
    {
        public int Id { get; set; }
        public string Homeaddress { get; set; }
        public int IsBed { get; set; }
        public string FacilitatorName { get; set; }
        
        public string MapPoint { get; set; }
    }
}
