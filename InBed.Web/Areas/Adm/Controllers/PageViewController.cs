using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using InBed.Core.Extentions;
using InBed.Service.Dto;
using InBed.Service.Request;
using InBed.Service.Abstracts;

namespace InBed.Web.Areas.Adm.Controllers
{
    public class PageViewController : AdmBaseController
    {

        #region Page
        
        // GET: Adm/PageView
        public ActionResult Index(int moudleId, int menuId, int btnId)
        {
            return View();
        }

        #endregion

        #region Ajax

        public JsonResult GetList(int moudleId, int menuId, int btnId)
        {
            var dataStr = Request[Request.Params.Keys[5]];
            var query = new PageViewRequest
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                OrderBy = Request["orderBy"],
                OrderDir = Request["orderDir"],
                LoginName= Request[Request.Params.Keys[4]],
                StartTime = StringExtension.IsBlank(dataStr) ? DateTime.MinValue : DateTime.Parse(dataStr.Substring(0, dataStr.IndexOf('-'))),
                EndTime = StringExtension.IsBlank(dataStr) ? DateTime.MaxValue : DateTime.Parse(dataStr.Substring(dataStr.IndexOf('-') + 1, 11))
            };

            var dto = pageViewService.GetWithPages(query);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}