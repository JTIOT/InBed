using InBed.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InBed.Web.Models
{
    public class AllotRequest
    {
       public List<BindingDto> bindings { get; set; }
        public string AleftFacilitatorCode { get; set; }
        public string RightFacilitatorCode { get; set; }

    }
}