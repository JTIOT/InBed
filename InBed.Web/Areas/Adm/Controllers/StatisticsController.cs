using InBed.Core.Extentions;
using InBed.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InBed.Web.Areas.Adm.Controllers
{
    public class StatisticsController : AdmBaseController
    {
        public ISleepingService sleepingService { get; set; }
        public IOnBedRecordService onBedRecordService { get; set; }

        public IHeartRateService heartRateService { get; set; }
        public IElderService elderService { get; set; }

        public IElderAnalysisService elderAnalysis { get; set; }

        // GET: Adm/Statistics
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ElderList(int moudleId, int menuId, int btnId)
        {
            
            return View();
        }
        public ActionResult HdStatistics(int moudleId, int menuId, int btnId)
        {

            return View();
        }
        public ActionResult ElderStatistics(int moudleId, int menuId, int btnId)
        {
           
            return View();
        }
        public ActionResult nightCountStatistics(int moudleId, int menuId, int btnId)
        {

            return View();
        }
        public ActionResult sleepStateStatistics(int moudleId, int menuId, int btnId)
        {

            return View();
        }
        public ActionResult PressureStatistics(int moudleId, int menuId, int btnId)
        {

            return View();
        }
        public ActionResult BRStatistics(int moudleId, int menuId, int btnId)
        {

            return View();
        }
        public ActionResult RestStatistics(int moudleId, int menuId, int btnId)
        {

            return View();
        }
        
        #region Ajax
        //一周睡眠情况
        [HttpGet]
        public JsonResult GetWeekSleepingDate(int elderId)
        {
            var dto = sleepingService.WeekSleepingDate(CurrentUser.FacilitatorCode, elderId);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        //半月在床记录
        [HttpGet]
        public JsonResult GetHalfMonthOnBedRecord(int elderId)
        {
            var dto = onBedRecordService.HalfMonthOnBedRecord(CurrentUser.FacilitatorCode, elderId);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        //心率数据
        [HttpGet]
        public JsonResult GetHeartRate(int elderId)
        {
            var dto = heartRateService.CurrentHeartRate(CurrentUser.FacilitatorCode, elderId);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
       //老人状态
        [HttpGet]
        public JsonResult GetElderStatistics(int elderId, string customerId)
        {
            var dto = elderService.GetElderStatistics(CurrentUser.FacilitatorCode, elderId, customerId);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        //历史心率数据
        [HttpGet]
        public JsonResult GetHistoryHeartRate()
        {
            string FacilitatorCode = CurrentUser.FacilitatorCode;
            string[] arr = Request["searchDateTime"].Split('-');
            string ElderName = Request["ElderName"];
            string S_data = string.Empty;
            string E_data = string.Empty;
            if (arr != null && arr.Length == 2)
            {
                S_data = arr[0].Replace('/', '-');
                E_data = arr[1].Replace('/', '-');
            }


            var dto = heartRateService.HistoryHeartRate(CurrentUser.FacilitatorCode, ElderName, S_data, E_data);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        //历史起夜数据
        [HttpGet]
        public JsonResult GetHistoryNightCount()
        {
            string FacilitatorCode = CurrentUser.FacilitatorCode;
            string[] arr = Request["searchDateTime"].Split('-');
            string ElderName = Request["ElderName"];
            string S_data = string.Empty;
            string E_data = string.Empty;
            if (arr != null && arr.Length == 2)
            {
                S_data = arr[0].Replace('/', '-');
                E_data = arr[1].Replace('/', '-');
            }


            var dto = elderAnalysis.ElderAnalysis(CurrentUser.FacilitatorCode, ElderName, S_data, E_data);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        //历史睡眠数据
        [HttpGet]
        public JsonResult GetHistorySleep()
        {
            string FacilitatorCode = CurrentUser.FacilitatorCode;
            string[] arr = Request["searchDateTime"].Split('-');
            string ElderName = Request["ElderName"];
            string S_data = string.Empty;
            string E_data = string.Empty;
            if (arr != null && arr.Length == 2)
            {
                S_data = arr[0].Replace('/', '-');
                E_data = arr[1].Replace('/', '-');
            }


            var dto = elderAnalysis.ElderAnalysis(CurrentUser.FacilitatorCode, ElderName, S_data, E_data);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取前一天中午12点后到现在的睡眠状态数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetCurrentDaySleepingDate(int elderId)
        {
            string FacilitatorCode = CurrentUser.FacilitatorCode;
            string ElderName = Request["ElderName"];
            var dto = elderService.GetCurrentDaySleepingDate(FacilitatorCode, elderId);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        ///历史呼吸
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetHistoryBreathing()
        {
            string FacilitatorCode = CurrentUser.FacilitatorCode;
            string[] arr = Request["searchDateTime"].Split('-');
            string ElderName = Request["ElderName"];
            string S_data = string.Empty;
            string E_data = string.Empty;
            if (arr != null && arr.Length == 2)
            {
                S_data = arr[0].Replace('/', '-');
                E_data = arr[1].Replace('/', '-');
            }
            var dto = heartRateService.HistoryBreathing(CurrentUser.FacilitatorCode, ElderName, S_data, E_data);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        ///历史重压值
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetHistoryWeight()
        {
            string FacilitatorCode = CurrentUser.FacilitatorCode;
            string[] arr = Request["searchDateTime"].Split('-');
            string ElderName = Request["ElderName"];
            string S_data = string.Empty;
            string E_data = string.Empty;
            if (arr != null && arr.Length == 2)
            {
                S_data = arr[0].Replace('/', '-');
                E_data = arr[1].Replace('/', '-');
            }
            var dto = heartRateService.HistoryWeight(CurrentUser.FacilitatorCode, ElderName, S_data, E_data);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        //历史作息数据
        [HttpGet]
        public JsonResult GetHistoryRest()
        {
            string FacilitatorCode = CurrentUser.FacilitatorCode;
            string[] arr = Request["searchDateTime"].Split('-');
            string ElderName = Request["ElderName"];
            string S_data = string.Empty;
            string E_data = string.Empty;
            if (arr != null && arr.Length == 2)
            {
                S_data = arr[0].Replace('/', '-');
                E_data = arr[1].Replace('/', '-');
            }


            var dto = elderAnalysis.ElderAnalysis(CurrentUser.FacilitatorCode, ElderName, S_data, E_data);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}