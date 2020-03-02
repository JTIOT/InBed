using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Entity
{
    //Refer to DailyAnalysisDAL
    public class DailyAnalysisEntity
    {
        public string keyId { get; set; }
        public string customerId { get; set; }
        public string gotoBedTime { get; set; }
        public string getupTime { get; set; }
        public string sleepingTime { get; set; }
        public int getupNum { get; set; }
        public string nightTimeAwakenings { get; set; }
        public string deepSleepTime { get; set; }
        public string sleepQuality { get; set; }
        public string maxHB { get; set; }
        public string maxHBTime { get; set; }
        public string minHB { get; set; }
        public string minHBTime { get; set; }
        public string isLoseSleep { get; set; }
        public string currDate { get; set; }
        public string sendStatus { get; set; }
        public string isNeedToSend { get; set; }
        public string isGetup { get; set; }
        public string operId { get; set; }
        public string operTime { get; set; }
        public string remark { get; set; }
        public string insertYmd { get; set; }
        public string insertId { get; set; }
        public string updateYmd { get; set; }
        public string updateId { get; set; }
        public string avgHB { get; set; }
        public string avgBR { get; set; }
        public string sleepLatency { get; set; }
        
    }
}