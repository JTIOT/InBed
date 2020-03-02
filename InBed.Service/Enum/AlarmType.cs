using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Enum
{
    public enum AlarmType
    {
        无心率 = 200,
        检测仪离线 = 201,
        检测板故障 = 202,
        床板接触不良=203,
        夜间离床过久 = 204,
        称重传感器故障=205,
        呼吸暂停现象 = 206,
        心率波形平坦 = 207,
        称重传感器重置 = 208,
        晚睡 = 209,
        早睡 = 210,
        晚起 = 211,
        卧床时间过久 = 212,
        频繁上下床 = 213,
        心动过速 = 214,
        心动过缓 = 215,
        主动求救 = 216,
        网络故障 = 217,
        网络恢复 = 218
    }
    public enum StrsHandle
    {
       未处理=0,
       手动处理=1

    }
}
