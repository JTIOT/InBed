using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Security;
using System.Xml;

namespace InBed.Core
{
    public static class ClassUtility
    {
        #region 判断手机

        public static bool IsMobile(this string str)
        {
            const string pattern = @"^(13|14|15|18|17)\d{9}$";
            if (string.IsNullOrEmpty(str))
                return false;
            return Regex.IsMatch(str, pattern);
        }

        #endregion
        public static int CalculateAge(DateTime birthDate, DateTime now)
        {
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
            {
                age--;
            }
            return age;
        }
        public static string MinuteToHour(int time)
        {
            int hour = 0;
            int minute = 0;
            if (time > 60)
            {
                minute = time / 60;
                minute = time % 60;
            }
            return (hour + "小时" + minute + "分钟");
        }
        public static string GetDeviceName(string number)
        {
            return number.ToString().Replace(":", "");
        }
        public static string GetTimeString(DateTime time)
        {
            return time.ToString().Substring(10,8);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string MD5(this string code)
        {
            string key1 = "羴毳舙";
            string key2 = "麤鱻赑";
            string key3 = "!@#)*(";
            string key4 = "$%^(*)";
            string md5 = FormsAuthentication.HashPasswordForStoringInConfigFile(key1 + code + key2, "MD5").ToUpper().Substring(0, 32);
            return FormsAuthentication.HashPasswordForStoringInConfigFile(key3 + md5 + key4, "MD5").ToLower().Substring(2, 26);
        }


        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>double</returns>
        public static int ToUinxTime(this DateTime time)
        {
            int intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = int.Parse((time - startTime).TotalSeconds.ToString().Split('.')[0]);
            return intResult;
        }

        /// <summary>
        /// Unix时间戳格式转换为DateTime时间格式
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this int timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp.ToString() + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// 字符串转换成时间
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string content)
        {
            try
            {
                if (string.IsNullOrEmpty(content))
                    return DateTime.Now;
                return Convert.ToDateTime(content);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// 转换成开始时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime ToBeginTime(this string time)
        {
            DateTime t;
            try
            {
                t = DateTime.Parse(Convert.ToDateTime(time).ToShortDateString());
            }
            catch (Exception)
            {
                t = DateTime.Parse(DateTime.Now.ToShortDateString());
            }
            return t;
        }

        /// <summary>
        /// 转换成结束日期
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime ToEndTime(this string time)
        {
            DateTime t;
            try
            {
                t = DateTime.Parse(Convert.ToDateTime(time).ToShortDateString()).AddDays(1);
            }
            catch (Exception)
            {
                t = DateTime.Parse(DateTime.Now.ToShortDateString()).AddDays(1);
            }
            return t;
        }

        public static List<TResult> ToList<TResult>(this DataTable dt) where TResult : class, new()
        {
            // 定义集合    
            IList<TResult> ts = new List<TResult>();

            // 获得此模型的类型   
            Type type = typeof(TResult);
            string tempName = "";

            foreach (DataRow dr in dt.Rows)
            {
                TResult t = new TResult();
                // 获得此模型的公共属性      
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;  // 检查DataTable是否包含此列    

                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter      
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts.ToList();
        }

        public static TResult ToModel<TResult>(this DataTable dt) where TResult : class, new()
        {
            //创建一个属性的列表
            var prlist = new List<PropertyInfo>();
            //获取TResult的类型实例　 反射的入口

            Type t = typeof(TResult);
            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表 
            Array.ForEach(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });
            if (dt.Rows.Count > 0)
            {
                var ob = new TResult();
                prlist.ForEach(p => { if (dt.Rows[0][p.Name] != DBNull.Value) p.SetValue(ob, dt.Rows[0][p.Name], null); });
                return ob;
            }
            return null;
        }

        /// <summary>
        /// 将XML转换为DataSet
        /// </summary>
        /// <param name="xmlcontent"></param>
        /// <returns></returns>
        public static DataSet ToDataSet(this XmlDocument xmlcontent)
        {
            try
            {
                StringReader reader = new StringReader(xmlcontent.InnerXml);
                XmlReader xml = XmlReader.Create(reader);
                DataSet ds = new DataSet();
                ds.ReadXml(xml);
                return ds;
            }
            catch
            {
                throw new Exception("系统异常，请稍后重试");
            }
        }
        /// <summary>
        /// 将字符串转换成xml文件格式
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static XmlDocument ToXmlDoc(this string content)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);
                return doc;
            }
            catch
            {
                throw new Exception("系统异常，请稍后重试");
            }
        }

        /// <summary>
        /// 字符串转换成浮点型
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static float ToFloat(this string content)
        {
            float number = 0;
            float.TryParse(content, out number);
            return number;
        }

        /// <summary>
        /// 字符串转换成整形
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static int ToInt32(this string content)
        {
            int number = 0;
            int.TryParse(content, out number);
            return number;
        }

        public static int ToInt32(this bool content)
        {
            if (content)
                return 1;
            return 0;
        }
        /// <summary>
        /// 字符串转换成长整形
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static long ToInt64(this string content)
        {
            long number = 0;
            long.TryParse(content, out number);
            return number;
        }
        /// <summary>
        /// 符串转换成Decimal
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string content)
        {
            decimal number = 0;
            decimal.TryParse(content, out number);
            return number;
        }
        /// <summary>
        /// 符串转换成双精度
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static double ToDouble(this string content)
        {
            double number = 0;
            double.TryParse(content, out number);
            return number;
        }

        /// <summary>
        /// 字符串转换成整形
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static int ToInt32(this Enum value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 字符串转换成布尔
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string content)
        {
            bool number = false;
            bool.TryParse(content, out number);
            return number;
        }

        /// <summary>
        /// 获得某个枚举项的描述
        /// </summary>
        /// <param name="value">要获取描述的枚举</param>
        /// <returns>枚举的描述</returns>
        public static string EnumDes(this Enum value)
        {
            FieldInfo fieldinfo = value.GetType().GetField(value.ToString());
            Object[] objs = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs == null || objs.Length == 0)
            {
                return value.ToString();
            }
            else
            {
                return ((DescriptionAttribute)objs[0]).Description;
            }
        }

        public static int ToObjInt(this object obj, int defaultvalue = 0)
        {
            if (obj == null)
                return defaultvalue;
            try
            {
                return ToInt32(obj.ToString());
            }
            catch
            {
                return defaultvalue;
            }
        }

        public static bool ToObjBoolean(this object obj, bool defaultvalue = false)
        {
            if (obj == null)
                return defaultvalue;
            try
            {
                return ToBoolean(obj.ToString());
            }
            catch
            {
                return defaultvalue;
            }
        }


        public static long ToObjLong(this object obj, long defaultvalue = 0)
        {
            if (obj == null)
                return defaultvalue;

            try
            {
                return ToInt64(obj.ToString());
            }
            catch
            {
                return defaultvalue;
            }
        }

        public static double ToObjDouble(this object obj, double defaultvalue = 0)
        {
            if (obj == null)
                return 0;
            return ToDouble(obj.ToString());
        }

        public static string ToObjStr(this object obj, string msg = "")
        {
            if (obj == null)
                return msg;
            return obj.ToString();
        }
    }
}
