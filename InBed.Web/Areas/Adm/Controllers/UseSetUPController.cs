using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using InBed.Core.Extentions;
using InBed.Service.Abstracts;
using InBed.Service.Dto;
using System.Web.Security;
using InBed.Service.Enum;
using InBed.Service.Request;

namespace InBed.Web.Areas.Adm.Controllers
{
    public class UseSetUPController : AdmBaseController
    {
        public IUseSetUPService useSetUPService { get; set; }
        // GET: Adm/UseSetUP
        #region Page
        public ActionResult Index(int moudleId, int menuId, int btnId)
        {
            GetButtons(menuId);
            return View();
        }
        public ActionResult Edit(int moudleId, int menuId, int btnId, int id)
        {
            var model = useSetUPService.GetDtail(id);
            return View(model);
        }
        #endregion

        #region Ajax
        [HttpGet]
        public JsonResult GetList(int moudleId, int menuId, int btnId)
        {
            var dateStr = Request[Request.Params.Keys[7]];
            var query = new UseSetUPRequest {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                OrderBy = Request["orderBy"],
                OrderDir = Request["orderDir"],
                FacilitatorCode = CurrentUser.FacilitatorCode,
                ElderName = Request[Request.Params.Keys[4]],
                ElderPhone = Request[Request.Params.Keys[5]],
                DataCount = Request[Request.Params.Keys[6]].ToInt(),
                StartTime = StringExtension.IsBlank(dateStr) ? DateTime.MinValue : DateTime.Parse(dateStr.Substring(0, dateStr.IndexOf('-'))),
                EndTime = StringExtension.IsBlank(dateStr) ? DateTime.MaxValue : DateTime.Parse(dateStr.Substring(dateStr.IndexOf('-') + 1, 11))                
            };            
            var dto = useSetUPService.GetWithPages(query);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Edit(string moudleId, string menuId, string btnId, SystemElderSetupDto dto)
        {
            var old = useSetUPService.GetDtail(dto.Id);
            old.start_data = dto.start_data;
            old.end_data = dto.end_data;
            old.is_enable = dto.is_enable;
            old.Modifier = CurrentUser.Id;
            useSetUPService.EditUseSet(old);
            return RedirectToAction("Index", RouteData.Values);
        }

        #endregion
    }
}