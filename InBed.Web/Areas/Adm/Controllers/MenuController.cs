using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using InBed.Core.Extentions;
using InBed.Service.Dto;
using InBed.Service.Enum;
using InBed.Service.Request;

namespace InBed.Web.Areas.Adm.Controllers
{
    public class MenuController : AdmBaseController
    {

        #region Page
        // GET: Adm/Menu
        public ActionResult Index(int moudleId, int menuId, int btnId)
        {
            GetButtons(menuId);
            return View();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public ActionResult Add(int moudleId, int menuId, int btnId)
        {
            ViewBag.ParentMenu = menuService.Query((int) MenuType.模块);
            return View();
        }

        public ActionResult Edit(int moudleId, int menuId, int btnId, int id)
        {
            ViewBag.ParentMenu = menuService.Query((int) MenuType.按钮);
            var model = menuService.GetDtail(id);
            return View(model);
        }

        #endregion

        #region Ajax

        [HttpPost]
        public ActionResult Add(string moudleId, string menuId, string btnId, MenuDto dto)
        {
            SetMenuType(ref dto);
            dto.Creater = CurrentUser.Id;
            dto.CreateDateTime = DateTime.Now;
            dto.Icon = "/abc";

            menuService.AddMenu(dto);
            return RedirectToAction("Index", RouteData.Values);
        }


        [HttpPost]
        public ActionResult Edit(string moudleId, string menuId, string btnId, MenuDto dto)
        {
            SetMenuType(ref dto);
            dto.Modifier = CurrentUser.Id;

            menuService.EditMenu(dto);
            return RedirectToAction("Index", RouteData.Values);
        }


        [HttpPost]
        public JsonResult Delete(string moudleId, string menuId, string btnId, List<int> ids)
        {
            var res = new Result<string>();

            if (ids != null && ids.Any())
            {
                var obj= menuService.DelMenu(ids);
                res.flag = obj.flag;
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        

        [HttpGet]
        public JsonResult GetList(string moudleId, string menuId, string btnId,string id)
        {
            var query = new MenuRequest
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                OrderBy = Request["orderBy"],
                OrderDir = Request["orderDir"],
                Name = Request[Request.Params.Keys[4]],
                Type = Request[Request.Params.Keys[5]].ToInt(),
            };
            var dto = menuService.GetWithPages(query);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        void SetMenuType(ref MenuDto dto)
        {
            var parentId = dto.ParentId;
            var parent = menuService.GetDtail(parentId);
            if (parentId<=0|| parent==null)
                dto.Type = MenuType.模块;
            else
            {
                switch (parent.Type)
                {
                    case MenuType.模块:
                        dto.Type = MenuType.菜单;
                        break;
                    case MenuType.菜单:
                        dto.Type = MenuType.按钮;
                        break;
                }
            }
        }

        #endregion
    }

    //public class MenuDto
    //{
    //    public string id { get; set; }

    //    public string name { get; set; }

    //    public string type { get; set; }

    //    public AdditionalParameters additionalParameters { get; set; }
    //}

    //public class AdditionalParameters
    //{
    //    public string id { get; set; }

    //    public bool children { get; set; }

    //    public bool itemSelected { get; set; }
    //}
}