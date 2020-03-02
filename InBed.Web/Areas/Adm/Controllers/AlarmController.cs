using InBed.Core.Extentions;
using InBed.Service.Abstracts;
using InBed.Service.Dto;
using InBed.Service.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InBed.Web.Areas.Adm.Controllers
{
    public class AlarmController  : AdmBaseController
    {
        public IAlarmService alarmService { get; set; }
        
        // GET: Adm/Alarm
        #region Page
        public ActionResult Index(int moudleId, int menuId, int btnId)
        {
            GetButtons(menuId);
            return View();
        }
        #endregion

        #region Ajax
        [HttpGet]
        public JsonResult GetList(string moudleId, string menuId, string btnId, string id)
        {
            var queryBase = new QueryBase
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                Draw = Request["draw"].ToInt(),
                SearchKey = Request["keywords"]
            };
            string []arr= Request[Request.Params.Keys[5]].Split('-');
            var query = new AlarmRequest
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                OrderBy = Request["orderBy"],
                OrderDir = Request["orderDir"],
                ElderName = Request[Request.Params.Keys[4]],
                FacilitatorCode = CurrentUser.FacilitatorCode
            };
            if (arr != null && arr.Length==2)
            {
                query.S_data = arr[0].Replace('/', '-');
                query.E_data = arr[1].Replace('/', '-');
            }
            var dto = alarmService.GetWithPages(query);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetCharData(string moudleId, string menuId, string btnId, string id)
        {

            string[] arr = Request[Request.Params.Keys[5]].Split('-');
            var query = new AlarmRequest
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                OrderBy = Request["orderBy"],
                OrderDir = Request["orderDir"],
                ElderName = Request["ElderName"],
        
                FacilitatorCode = CurrentUser.FacilitatorCode
            };
            if (arr != null && arr.Length == 2)
            {
                query.S_data = arr[0].Replace('/', '-');
                query.E_data = arr[1].Replace('/', '-');
            }
            var dto = alarmService.GetWithPages(query);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}