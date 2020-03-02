using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Dto
{
    public class ElderHeartRate
    {
        private DateTime oldtime=DateTime.Now;
        private int id, avHR, flag;
        private string time = string.Empty;
        private List<HeartRateData> res;
        private static IDictionary<int, HeartRateData> HeartRateData = new Dictionary<int, HeartRateData>();
        private Queue<int> QueueList = new Queue<int>();
        private IDictionary<int, ElderHeartRate> HeartRateMap = new Dictionary<int, ElderHeartRate>();

        public ElderHeartRate(int id,int avHR,string time)
        {
            this.avHR = avHR;
            this.id = id;
            this.time = time;
            
            res = new List<Dto.HeartRateData>();
        }
        public void SetData(int rowid,int value, string time)
        {
           
            if (!HeartRateData.ContainsKey(rowid))
            {
                flag = 1;
                QueueList.Enqueue(value);
                if (QueueList.Count > 5)
                {
                    QueueList.Dequeue();
                }
                List<int> tmp = new List<int>();
                tmp.AddRange(QueueList.ToList());
                tmp.Sort();
                tmp.Remove(0);
                tmp.Sort((x, y) => -x.CompareTo(y));
                tmp.Remove(0);
                this.avHR = (int)tmp.Average();
                this.time = time;
                oldtime = DateTime.Parse(time);
                HeartRateData itemHR = new HeartRateData
                {
                    time = time,
                    value = avHR.ToString()
                };
                res.Add(itemHR);
                HeartRateData.Add(rowid, itemHR);
            }

        }
        public int ElderID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public int Flag
        {   get {
                return flag;
            }
            set {
                flag = value;
            }
        }
        public int Value
        {
            get
            {
                return avHR;
            }
            set
            {
                avHR = value;
            }
        }
        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
            }
        }

        public List<HeartRateData> ListHD {
            get
            {
                return res;
            }
            set
            {
                res = value;
            }

        }



    }
    /// <summary>
    /// 老人呼吸
    /// </summary>
    public class ElderBR
    {
        private int id, brvalue, flag;
        private string time = string.Empty;
        public IDictionary<int, BreathingRate> BRMap = new Dictionary<int, BreathingRate>();
        private List<BreathingRate> res;
        public ElderBR(int id, int value, string time)
        {
            this.brvalue = value;
            this.id = id;
            this.time = time;
            res = new List<Dto.BreathingRate>();
            if (value != 0)
            {

                res.Add(new BreathingRate { id = id, value = value.ToString(), time = time, Flag = 1 });
                BRMap.Add(id, new BreathingRate { id = id, value = value.ToString(), time = time, Flag = 1 });
            }
               
        }
        public void SetData(int rowid, int value, string time)
        {
            if (!BRMap.ContainsKey(rowid))
            {
                if(value!=0)
                {
                    BreathingRate itemBR = new BreathingRate { id = rowid, value = value.ToString(), time = time, Flag = 1 };
                    res.Add(itemBR);
                    BRMap.Add(rowid, itemBR);
                }
                
            }

        }
        public List<BreathingRate> ListBR
        {
            get
            {
                return res;
            }
            set
            {
                res = value;
            }

        }
    }

    public class ElderPressure
    {
        private int id, brvalue, flag;
        private string time = string.Empty;
        public IDictionary<int, Pressure> PMap = new Dictionary<int, Pressure>();
        private List<Pressure> res;
        public ElderPressure(int id, int value, string time)
        {
            this.brvalue = value;
            this.id = id;
            this.time = time;
            res = new List<Dto.Pressure>();
            if (value != 0)
            {
                res.Add(new Pressure { id = id, value = value.ToString(), time = time, Flag = 1 });

                PMap.Add(id, new Pressure { id = id, value = value.ToString(), time = time, Flag = 1 });
            }
               
        }
        public void SetData(int rowid, int value, string time)
        {
            if (!PMap.ContainsKey(rowid))
            {
                if (value != 0)
                {
                    Pressure item = new Pressure { id = rowid, value = value.ToString(), time = time, Flag = 1 } ;
                    res.Add(item);
                    PMap.Add(rowid, item);
                }

            }

        }
        public List<Pressure> ListPressure
        {
            get
            {
                return res;
            }
            set
            {
                res = value;
            }

        }
    }
}
