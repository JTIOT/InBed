using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data
{
    public class SelectBuilder
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int Top { get; set; }

        public string Having { get; set; }

        public string GroupBy { get; set; }

        public string OrderBy { get; set; }
 
        public string TableName { get; set; }

        public string Column { get; set; }

        public string WhereSql { get; set; }

        public string OrderDir { get; set; }
    }
}
