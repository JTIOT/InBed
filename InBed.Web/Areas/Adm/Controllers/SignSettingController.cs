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
    public class SignSettingController : AdmBaseController
    {
        public IElderAlarmSetupService alarmSetupService { get; set; }
        #region Page

        // GET: Adm/User
        public ActionResult Index(int moudleId, int menuId, int btnId)
        {
            GetButtons(menuId);
            return View();
        }
        public ActionResult Edit(int moudleId, int menuId, int btnId, int id)
        {
            var model = alarmSetupService.GetDeteail(id);
            return View(model);
        }
        #endregion
        #region Ajax
        [HttpGet]
        public JsonResult GetList(string moudleId, string menuId, string btnId, string id)
        {
            var query = new AlarmSetupRequest
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                OrderBy = Request["orderBy"],
                OrderDir = Request["orderDir"],
                ElderName = Request[Request.Params.Keys[4]],
                ElderPhone= Request[Request.Params.Keys[5]],
                FacilitatorCode = CurrentUser.FacilitatorCode
            };
            var dto = alarmSetupService.GetWithPages(query);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(int moudleId, int menuId, int btnId, AlarmSetupDto dto)
        {
            alarmSetupService.EditAlarmSetup(dto);
            return RedirectToAction("Index", RouteData.Values);
        }
        #endregion
    }
}