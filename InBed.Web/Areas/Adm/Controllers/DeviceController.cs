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
using InBed.Web.Models;

namespace InBed.Web.Areas.Adm.Controllers
{
    public class DeviceController : AdmBaseController
    {
        public IDeviceService deviceService { get; set; }
        public IFacilitatorService facilitatorService { get; set; }
        public IDeviceBindingService bindingService { get; set; }

        public IElderService elderService { get; set; }

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
         
            var model = deviceService.GetDeteail(id);
            return View(model);
        }
        public ActionResult Allot(int moudleId, int menuId, int btnId)
        {
            ViewBag.Facilitator = facilitatorService.GetlowerFacilitator(CurrentUser.FacilitatorCode);
            return View();
        }
        #endregion

        #region Ajax
        [HttpPost]
        public ActionResult Add(string moudleId, string menuId, string btnId, DeviceDto dto)
        {
            SetDeviceType(ref dto);
            deviceService.AddDevice(dto);
            return RedirectToAction("Index", RouteData.Values);
        }

        [HttpPost]
        public ActionResult Edit(string moudleId, string menuId, string btnId, DeviceDto dto)
        {
            var old = deviceService.GetDeteail(dto.Id);
            old.device_model = dto.device_model;
            old.device_type = dto.device_type;
            old.is_enable = dto.is_enable;
            old.status = dto.status;
            var user = User.Identity as FormsIdentity;
            old.Modifier =Convert.ToInt32(user.Ticket.UserData);

            deviceService.EditDevice(old);
            return RedirectToAction("Index", RouteData.Values);
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
            var query = new DeviceRequest
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                OrderBy = Request["orderBy"],
                OrderDir = Request["orderDir"],
                Number = Request[Request.Params.Keys[4]],
                DeviceType = Request[Request.Params.Keys[5]].ToInt(),
                model = Request[Request.Params.Keys[6]],
                DeviceState= Request[Request.Params.Keys[7]].ToInt(),
                FacilitatorCode = CurrentUser.FacilitatorCode
            };
            var dto = deviceService.GetWithPages(query);          
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(string moudleId, string menuId, string btnId, List<int> ids)
        {
            var res = new Result<string>();

            if (ids != null && ids.Any())
            {
                var obj= deviceService.DelDevice(ids);
                res.flag = obj.flag;
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        void SetDeviceType(ref DeviceDto dto)
        {
            var parentId = dto.Id;
            var parent = deviceService.GetDeteail( parentId);
            if (parentId <= 0 || parent == null)
            {
                dto.device_type =DeviceType.智能垫片;
                dto.status = DeviceStatus.正常使用;
            }
            else
            {
                switch (parent.device_type)
                {
                    case DeviceType.智能垫片:
                        dto.device_type = DeviceType.智能垫片;
                        break;
                    case DeviceType.智能音箱:
                        dto.device_type = DeviceType.智能音箱;
                        break;
                    case DeviceType.智能传感器:
                        dto.device_type = DeviceType.智能传感器;
                        break;
                }
                dto.status = DeviceStatus.正常使用;
            }
            var user = User.Identity as FormsIdentity;
            var onlineUserID = Convert.ToInt32(user.Ticket.UserData);
            dto.Creater = onlineUserID;
            dto.FacilitatorCode = user.Name.Split('#')[0];
            dto.FacilitatorName = user.Name.Split('#')[1];




        }

        /// <summary>
        /// 左转移
        /// </summary>
        /// <param name="moudleId"></param>
        /// <param name="menuId"></param>
        /// <param name="btnId"></param>
        /// <param name="id"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AleftMetastasis(string moudleId, string menuId, string btnId, string id, List<BindingDto> bindings)
        {

            var obj = deviceService.Metastasis(bindings, bindings.FirstOrDefault().FacilitatorName);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 右转移
        /// </summary>
        /// <param name="moudleId"></param>
        /// <param name="menuId"></param>
        /// <param name="btnId"></param>
        /// <param name="id"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RightMetastasis(int moudleId, int menuId, int btnId, int id, List<BindingDto> bindings)
        {
            var obj = deviceService.Metastasis(bindings, bindings.FirstOrDefault().FacilitatorName);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAleftElder(int moudleId, int menuId, int btnId)
        {
            var query = new DeviceBindingRequest
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                OrderBy = Request["orderBy"],
                OrderDir = Request["orderDir"],
                FacilitatorCode = Request[Request.Params.Keys[4]]==""?CurrentUser.FacilitatorCode: Request[Request.Params.Keys[4]]
            };
            var dto = bindingService.GetWithPages(query);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetRightElder(int moudleId, int menuId, int btnId)
        {
            var query = new DeviceBindingRequest
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                OrderBy = Request["orderBy"],
                OrderDir = Request["orderDir"],
                FacilitatorCode = Request[Request.Params.Keys[5]] == "" ? CurrentUser.FacilitatorCode : Request[Request.Params.Keys[5]]
            };
            var dto = bindingService.GetWithPages(query);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取服务商
        /// </summary>
        /// <param name="moudleId"></param>
        /// <param name="menuId"></param>
        /// <param name="btnId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetFacilitator(int moudleId, int menuId, int btnId)
        {
            var dto = facilitatorService.GetlowerFacilitator(CurrentUser.FacilitatorCode);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}