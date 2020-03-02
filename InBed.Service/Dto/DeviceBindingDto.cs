using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Dto
{
    public class DeviceBindingDto : BaseDto
    {
        public long DeviceId { get; set; }
        public long ElderId { get; set; }
    }
}
