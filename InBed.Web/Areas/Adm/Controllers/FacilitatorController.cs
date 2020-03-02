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
    public class FacilitatorController : AdmBaseController
    {
        // GET: Adm/Facilitator
        public IFacilitatorService facilitatorService { get; set; }
        #region Page
        public ActionResult Index(int moudleId, int menuId, int btnId)
        {
            GetButtons(menuId);
            return View();
        }
        public ActionResult Add(string moudleId, string menuId, string btnId)
        {
            return View();
        }


        public ActionResult Edit(int moudleId, int menuId, int btnId, int id)
        {
            var model = facilitatorService.GetDeteail(id);
            return View(model);
        }
        #endregion

        #region Ajax

        [HttpPost]
        public ActionResult Add(string moudleId, string menuId, string btnId, FacilitatorDto dto)
        {
            SetUpFacilitator(ref dto);
           
            facilitatorService.AddFacilitator(dto);
            return RedirectToAction("Index", RouteData.Values);
        }

        [HttpPost]
        public ActionResult Edit(string moudleId, string menuId, string btnId, FacilitatorDto dto)
        {
            var old = facilitatorService.GetDeteail(dto.Id);
            old.Name = dto.Name;
            old.Number = dto.Number;
            old.Contacts = dto.Contacts;
            old.Province = dto.Province;
            old.City = dto.City;
            old.District = dto.District;
            old.Address = dto.Address;
            old.Capital = dto.Capital;
            old.Management = dto.Management;
           // old.UP_id = dto.UP_id;
            old.Modifier =CurrentUser.Id;
          

            facilitatorService.EditFacilitator(old);
            return RedirectToAction("Index", RouteData.Values);
        }
        [HttpGet]
        public JsonResult GetList(string moudleId, string menuId, string btnId, string id)
        {
            var dataStr = Request[Request.Params.Keys[7]];
            var query = new FacilitatorRequest
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                OrderBy = Request["orderBy"],
                OrderDir = Request["orderDir"],
                Name = Request[Request.Params.Keys[4]],
                 Number= Request[Request.Params.Keys[5]],
                Adders = Request[Request.Params.Keys[6]],
                 FacilitatorCode=CurrentUser.FacilitatorCode,
                StartTime = StringExtension.IsBlank(dataStr) ? DateTime.MinValue : DateTime.Parse(dataStr.Substring(0, dataStr.IndexOf('-'))),
                EndTime = StringExtension.IsBlank(dataStr) ? DateTime.MaxValue : DateTime.Parse(dataStr.Substring(dataStr.IndexOf('-') + 1, 11))
            };
            var dto = facilitatorService.GetWithPages(query);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(string moudleId, string menuId, string btnId, List<int> ids)
        {
            var res = new Result<string>();

            if (ids != null && ids.Any())
            {
                var obj= facilitatorService.DelFacilitator(ids);
                res.flag = obj.flag;
            }            
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        void SetUpFacilitator(ref FacilitatorDto dto)
        {
            //待写，获取上级供应商
            var UPFacilitator = CurrentUser.FacilitatorCode;
           // var parent = facilitatorService.GetOne(item => item.Code.Equals(UPFacilitator));
           var parent = facilitatorService.Query(UPFacilitator);

            if (parent != null)
            {
                dto.UP_id = parent.Id;
            }
            else
            {
                dto.UP_id = 0;
            }
        }

        #endregion


    }
}