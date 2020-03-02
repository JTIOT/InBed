using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using InBed.Core.Extentions;
using InBed.Service.Dto;
using InBed.Service.Request;
using InBed.Service.Abstracts;

namespace InBed.Web.Areas.Adm.Controllers
{
    public class DeviceBindingController : AdmBaseController
    {
        #region Page
        public IDeviceBindingService bindingService { get; set; }
        // GET: Adm/PageView
        public ActionResult Index(int moudleId, int menuId, int btnId)
        {
            return View();
        }
        public ActionResult Edit(int moudleId, int menuId, int btnId, int id)
        {
            var model = bindingService.GetDeteail(id);
            return View(model);
        }
        #endregion
        #region Ajax

        public JsonResult GetList(int moudleId, int menuId, int btnId)
        {
            var query = new DeviceBindingRequest
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                OrderBy = Request["orderBy"],
                OrderDir = Request["orderDir"],
                ElderName = Request[Request.Params.Keys[4]],
                ElderPhone = Request[Request.Params.Keys[5]],
                DeviceNumber = Request[Request.Params.Keys[6]],
                DeviceType = Request[Request.Params.Keys[7]].ToInt(),
                FacilitatorCode = CurrentUser.FacilitatorCode
            };

            var dto = bindingService.GetWithPages(query);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        #endregion


    }
}