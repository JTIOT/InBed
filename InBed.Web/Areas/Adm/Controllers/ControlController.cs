using System.Web.Mvc;
using InBed.Service.Abstracts;
using InBed.Core.Extentions;

namespace InBed.Web.Areas.Adm.Controllers
{
    public class ControlController : AdmBaseController
    {

        public IRoleMenuService roleMenuService { get; set; }
        public IUserRoleService userRoleService { get; set; }
        public IElderService elderService { get; set; }
        public IAlarmService alarmService { get; set; }
        // GET: Adm/Control
        public ActionResult Index()
        {
            if (IsLogined)
            {
                //获取拥有的角色
                var userid = CurrentUser.Id;
                ViewBag.MyMenus = userService.GetMyMenus(userid);
            }
            return View();
        }
        /// <summary>
        /// Welcome
        /// </summary>
        /// <returns></returns>
        public ActionResult Welcome()
        {
            return View();
        }
        #region Ajax
        //[HttpGet]
        //public JsonResult GetMapPosition()
        //{
        //    var dto = elderService.GetMapPosition(CurrentUser.FacilitatorCode);
        //    return Json(dto, JsonRequestBehavior.AllowGet);
        //}
        [HttpGet]
        public JsonResult GetMapPosition(string province)
        {
            var dto = elderService.GetMapPosition(CurrentUser.FacilitatorCode, province);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAgeGroup()
        {
            var dto = elderService.AgeGroup(CurrentUser.FacilitatorCode);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetElderCount()
        {
            var dto = elderService.GetElderCount(CurrentUser.FacilitatorCode);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAlarm()
        {
            var dto = alarmService.GetAlarm(CurrentUser.FacilitatorCode);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetMessage()
        {
            var dto = alarmService.GetMessage(CurrentUser.FacilitatorCode);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetPlaceNameeNumber()
        {
            var dto = elderService.GetPlaceNameeNumber(CurrentUser.FacilitatorCode);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetPlaceOnlineNumber()
        {
            var dto = elderService.GetPlaceOnlineNumber(CurrentUser.FacilitatorCode);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetOutBedNumber()
        {
            var dto = alarmService.GetMessage(CurrentUser.FacilitatorCode);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult HandleAlarm()
        {

            int alarmID = Request["alarmID"].ToInt();
            string mag = Request["mag"];
            var dto = alarmService.HandleAlarm(alarmID, CurrentUser.Id

, mag);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}