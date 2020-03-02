using System.Collections.Generic;
using InBed.Entity.Base;
using Newtonsoft.Json;
using System;

namespace InBed.Entity
{
   public class DeviceBindingEntity : BaseEntity
    {
        public int ElderId { get; set; }
        public int DeviceId { get; set; }
         
    }
}
