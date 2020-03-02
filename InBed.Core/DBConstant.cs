using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Core
{
    public static class DBConstant
    {
        public static readonly string SqlR = ConfigurationManager.ConnectionStrings["sqlr"].ConnectionString;
        public static readonly string SqlW = ConfigurationManager.ConnectionStrings["sqlw"].ConnectionString;
        public static readonly string BalSqlR = ConfigurationManager.ConnectionStrings["BalSqlR"].ConnectionString;
        public static readonly string BalSqlW = ConfigurationManager.ConnectionStrings["BalSqlW"].ConnectionString;
    }
}
