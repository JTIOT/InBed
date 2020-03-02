using InBed.Service.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Dto
{
    public class AlarmDto
    {
        public int ID { get; set; }
        public int ElderID { get; set; }
        public string DeviceNumber { get; set; }
        public AlarmType AlarmType { get; set; }
        public string AlarmTime { get; set; }
        public string UploadTime { get; set; }
        public string OnLineTime { get; set; }
        public string AlarmReason { get; set; }
        public StrsHandle IsHandle { get; set; }
        public string HandleName
        {
            get
            {
                return IsHandle.ToString();
            }
        }
        public string HandleOpinions { get; set; }
        public int Modifier { get; set; }
        public string AlarmTypeName
        {

            get {
                return AlarmType.ToString();
            }
        }
        public string ElderName { get; set; }
        public string AlarmColor
        {

            get
            {
                string color = "";
                switch (AlarmType)
                {
                    case AlarmType.主动求救:
                        color = "#FF0000";
                        break;
                    case AlarmType.卧床时间过久:
                        color = "#FF7F50";
                        break;
                    case AlarmType.夜间离床过久:
                        color = "#FFD306";
                        break;
                    case AlarmType.床板接触不良:
                        color = "#F75000";
                        break;
                    case AlarmType.心动过缓:
                        color = "#AD5A5A";
                        break;
                    case AlarmType.心动过速:
                        color = "#FF1493";
                        break;
                    case AlarmType.心率波形平坦:
                        color = "#7FFF00";
                        break;
                    case AlarmType.无心率:
                        color = "#9400D3";
                        break;
                    case AlarmType.早睡:
                        color = "#C6A300";
                        break;
                    case AlarmType.晚睡:
                        color = "#CECEFF";
                        break;
                    case AlarmType.晚起:
                        color = "#80FFFF";
                        break;
                    case AlarmType.检测仪离线:
                        color = "#BBFFBB";
                        break;
                    case AlarmType.检测板故障:
                        color = "#ff7575";
                        break;
                    case AlarmType.称重传感器故障:
                        color = "#BE77FF";
                        break;
                    case AlarmType.称重传感器重置:
                        color = "#007979";
                        break;
                    case AlarmType.网络恢复:
                        color = "#00DB00";
                        break;
                    case AlarmType.网络故障:
                        color = "#B7FF4A";
                        break;
                    case AlarmType.频繁上下床:
                        color = "#46A3FF";
                        break;                 
                }
                return color;
            }
        }
    }
}
