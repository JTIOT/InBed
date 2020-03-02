using System.Collections.Generic;
using InBed.Entity.Base;
using Newtonsoft.Json;
using System;

namespace InBed.Entity
{
    public class DeviceAllotEntity : BaseEntity
    {
        public int deviceID { get; set; }
        public string up_facilitator { get; set; }
        public string facilitator { get; set; }
    }
}
