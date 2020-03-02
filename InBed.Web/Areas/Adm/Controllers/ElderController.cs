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
    public class ElderController : AdmBaseController
    {
        // GET: Adm/Facilitator
        public IElderService elderService { get; set; }
        #region Page
        // GET: Adm/Elder
        public ActionResult Index(int moudleId, int menuId, int btnId)
        {
            GetButtons(menuId);
            return View();
        }
        public ActionResult Add(string moudleId, string menuId, string btnId)
        {
            return View();
        }
        public ActionResult ElderQueryR(string moudleId, string menuId, string btnId)
        {
            return View();
        }
        
        public ActionResult Edit(int moudleId, int menuId, int btnId, int id)
        {
            var model = elderService.GetDeteail(id);
            return View(model);
        }
        public ActionResult ElderList(int moudleId, int menuId, int btnId)
        {
            var listDto= elderService.GetWithPages(new ElderRequest { Start = 0, Length = 5000, FacilitatorCode = CurrentUser.FacilitatorCode });
            ViewBag.Elder = listDto.data;
            return View();
        }
        #endregion

        #region Ajax
        [HttpPost]
        public ActionResult Add(string moudleId, string menuId, string btnId, ElderDto dto)
        {
            dto.FacilitatorCode = CurrentUser.FacilitatorCode;
            dto.FacilitatorName = CurrentUser.FacilitatorName;
            dto.Creater = CurrentUser.Creater;
            dto.CreateDateTime = DateTime.Now;

            elderService.AddElder(dto);
            return RedirectToAction("Index", RouteData.Values);
        }
        [HttpPost]
        public ActionResult Add123(ElderDto dto)
        {
            dto.FacilitatorCode = CurrentUser.FacilitatorCode;
            dto.FacilitatorName = CurrentUser.FacilitatorName;
            dto.Creater = CurrentUser.Creater;
            dto.CreateDateTime = DateTime.Now;

            elderService.AddElder(dto);
            return RedirectToAction("Index", RouteData.Values);
        }
        [HttpPost]
        public ActionResult Edit(string moudleId, string menuId, string btnId, ElderDto dto)
        {
            var old = elderService.GetDeteail(dto.Id);
            old.birthday = dto.birthday;
            old.blood_type = dto.blood_type;
            old.contacts = dto.contacts;
            old.c_telephone = dto.c_telephone;
            old.deformity_number = dto.deformity_number;
            old.dispensedmode = dto.dispensedmode;
            old.education = dto.education;
            old.genetic_disease = dto.genetic_disease;
            old.homeaddress = dto.homeaddress;
            old.homephone = dto.homephone;
            old.H_allergydrug = dto.H_allergydrug;
            old.H_disease = dto.H_disease;
            old.is_deformity = dto.is_deformity;
            old.is_genetic = dto.is_genetic;
            old.is_ops = dto.is_ops;
            old.marriageStatus = dto.marriageStatus;
            old.name = dto.name;
            old.nation = dto.nation;
            old.number = dto.number;
            old.occupation = dto.occupation;
            old.ops_data = DateTime.Parse("1900-1-1");
            old.ops_disease = dto.ops_disease;
            old.photo = dto.photo;
            old.resident_type = dto.resident_type;
            old.sex = dto.sex;
            old.workunit = dto.workunit;
            old.Modifier = CurrentUser.Id;


            elderService.EditElder(old);
            return RedirectToAction("Index", RouteData.Values);
        }
        [HttpGet]
        public JsonResult GetElderList(string moudleId, string menuId, string btnId, string id)
        {
            var query = new ElderRequest
            {

            };
            var dto = elderService.GetWithPages(query);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetElderPages(string moudleId, string menuId, string btnId, string id)
        {
            var query = new ElderRequest
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                OrderBy = Request["orderBy"],
                OrderDir = Request["orderDir"],
                FacilitatorCode =CurrentUser.FacilitatorCode
            };
            var dto = elderService.GetElderPages(query);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(string moudleId, string menuId, string btnId, List<int> ids)
        {
            var res = new Result<string>();

            if (ids != null && ids.Any())
            {
                var dto = elderService.DelElder(ids);
                res.flag = dto.flag;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

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
            var dateStr = Request[Request.Params.Keys[7]];
            var query = new ElderRequest {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                OrderBy = Request["orderBy"],
                OrderDir = Request["orderDir"],
                FacilitatorCode = CurrentUser.FacilitatorCode,
                Name = Request[Request.Params.Keys[4]],
                HomePhone= Request[Request.Params.Keys[5]],
                Homeaddress= Request[Request.Params.Keys[6]],
                StartTime = StringExtension.IsBlank(dateStr) ? DateTime.MinValue : DateTime.Parse(dateStr.Substring(0, dateStr.IndexOf('-'))),
                EndTime = StringExtension.IsBlank(dateStr) ? DateTime.MaxValue : DateTime.Parse(dateStr.Substring(dateStr.IndexOf('-') + 1, 11)),               
            };
            var dto = elderService.GetWithPages(query);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ElderQuery(string id)
        {
            var query = new ElderRequest
            {
                FacilitatorCode = CurrentUser.FacilitatorCode,
                Name = Request["queryValue"],
                 Start=0,
                  Length=Int32.MaxValue
            };
            var dto = elderService.ElderQuery(query);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 老人详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult ElderDeteail(string id)
        {
            var dto = elderService.GetDeteail(id.ToInt());
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ElderDetails()
        {
            var dto = elderService.GetDetails();
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

       

        #endregion
    }
}