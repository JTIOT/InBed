using System.Collections.Generic;

namespace InBed.Web.Models
{
    public class AuthMenuDto
    {
        public List<int> RoleIds { get; set; } 

        public List<int> MenuIds { get; set; } 
    }
}