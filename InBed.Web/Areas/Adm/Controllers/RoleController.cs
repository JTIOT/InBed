using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using InBed.Core.Extentions;
using InBed.Service.Abstracts;
using InBed.Service.Dto;
using InBed.Web.Models;
using InBed.Service.Request;

namespace InBed.Web.Areas.Adm.Controllers
{
    public class RoleController : AdmBaseController
    {
        public IRoleService roleService { set; get; }

        public IRoleMenuService roleMenuService { get; set; }

        

        #region Page

        // GET: Adm/Role
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
            var model = roleService.Detail(id);
            return View(model);
        }

        public ActionResult AuthMenus(int moudleId, int menuId, int btnId)
        {
            var query = new MenuRequest
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                OrderBy = Request["orderBy"],
                OrderDir = Request["orderDir"],
            };

            var res = menuService.GetWithPages(query);
            ViewBag.Menus = res.data;
            return View();
        }

        #endregion

        #region Ajax

        public JsonResult GetList(int moudleId, int menuId, int btnId)
        {

            var query = new RoleRequest
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                OrderBy = Request["orderBy"],
                OrderDir = Request["orderDir"],
                Name= Request[Request.Params.Keys[4]],
                Description= Request[Request.Params.Keys[5]],
                FacilitatorCode = CurrentUser.FacilitatorCode

            };
            var dto = roleService.GetWithPages(query);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Add(int moudleId, int menuId, int btnId, RoleDto dto)
        {
            dto.Creater = CurrentUser.Id;
            dto.CreateDateTime = DateTime.Now;
            dto.FacilitatorCode = CurrentUser.FacilitatorCode;
            dto.FacilitatorName = CurrentUser.FacilitatorName;
            roleService.AddRole(dto);
            return RedirectToAction("Index", RouteData.Values);
        }

        [HttpPost]
        public ActionResult Edit(int moudleId, int menuId, int btnId, RoleDto dto)
        {
            roleService.EditRole(dto);
            return RedirectToAction("Index", RouteData.Values);
        }


        [HttpPost]
        public JsonResult Delete(int moudleId, int menuId, int btnId, List<int> ids)
        {
            var res = new Result<string>();
            if (ids != null && ids.Any())
            {
                var obj= roleService.DelRole(ids);
                res.flag = obj.flag;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AuthMenus(int moudleId, int menuId, int btnId, AuthMenuDto dto)
        {
            var res = new Result<int>();

            foreach (var roleId in dto.RoleIds)
            {
                var newRoleMenus = dto.MenuIds.Select(item => new RoleMenuDto { RoleId = roleId, MenuId = item, Creater = CurrentUser.Id, CreateDateTime = DateTime.Now }).ToList();
                roleMenuService.AddRoleMenu(newRoleMenus, roleId);
            }

            res.flag = true;

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetRoleMenusByRoleId(int moudleId, int menuId, int btnId, int id)
        {
            var res = new Result<List<RoleMenuDto>>();
            var list = roleMenuService.Query(id);
            res.flag = true;
            res.data = list;
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}