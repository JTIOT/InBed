using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using InBed.Core.Extentions;
using InBed.Service.Abstracts;
using InBed.Service.Dto;
using InBed.Service.Request;

namespace InBed.Web.Areas.Adm.Controllers
{
    public class LoginlogController : AdmBaseController
    {
        public ILoginLogService loginLogService { set; get; }

        #region Page

        // GET: Adm/LoginLog
        public ActionResult Index(string moudleId, string menuId, string btnId)
        {
            return View();
        }

        #endregion

        #region Ajax

        public JsonResult GetList(string moudleId, string menuId, string btnId)
        {
    


            var dataStr = Request[Request.Params.Keys[5]];
            var query = new LogRequest
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                OrderBy = Request["orderBy"],
                OrderDir = Request["orderDir"],
                LoginName = Request[Request.Params.Keys[4]],
                StartTime = StringExtension.IsBlank(dataStr) ? DateTime.MinValue : DateTime.Parse(dataStr.Substring(0, dataStr.IndexOf('-'))),
                 EndTime = StringExtension.IsBlank(dataStr) ? DateTime.MaxValue : DateTime.Parse(dataStr.Substring(dataStr.IndexOf('-')+1,11))


            };

            var dto = loginLogService.GetWithPages(query);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}