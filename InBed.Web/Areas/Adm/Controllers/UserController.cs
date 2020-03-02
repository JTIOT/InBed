using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using InBed.Core.Extentions;
using InBed.Service.Abstracts;
using InBed.Service.Dto;
using System.Web.Security;
using InBed.Service.Request;

namespace InBed.Web.Areas.Adm.Controllers
{
    public class UserController : AdmBaseController
    {
        public IUserRoleService userRoleService { get; set; }
   


        #region Page

        // GET: Adm/User
        public ActionResult Index(int moudleId, int menuId, int btnId)
        {
            GetButtons(menuId);
            return View();
        }
        //public ActionResult Index()
        //{      
        //    return View();
        //}
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public ActionResult Add(int moudleId, int menuId, int btnId)
        {
            return View();
        }

        public ActionResult Edit(int moudleId, int menuId, int btnId, int id)
        {
            var model = userService.GetDtail(id);
            return View(model);
        }

        /// <summary>
        /// 用户角色授权
        /// </summary>
        /// <param name="moudleId"></param>
        /// <param name="menuId"></param>
        /// <param name="btnId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Authen(int moudleId, int menuId, int btnId, int id)
        {
            return View();
        }

        // GET: Adm/User/Login
        [HttpGet, AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ForgotPwd()
        {
            return View();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Reg()
        {
            return View();
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                userService.Logout();
            }
            return RedirectToAction("Login");
        }

        #endregion

        #region Ajax

        [HttpGet]
        public JsonResult GetList(int moudleId, int menuId, int btnId)
        {
            string ss = Request[Request.Params.Keys[7]];
            var query = new GetUserRequest
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                OrderBy = Request["orderBy"],
                OrderDir = Request["orderDir"],
                FacilitatorCode = CurrentUser.FacilitatorCode,
                UserName = Request[Request.Params.Keys[4]],
                RealName = Request[Request.Params.Keys[5]],
                UserState = Request[Request.Params.Keys[6]].ToInt(),
                StartTime =DateTime.MinValue,
                EndTime= DateTime.MaxValue
            };
            var dto = userService.GetWithPages(query);
            var res = new ResultDto<UserDto>
            {
                start = dto.start,
                length = dto.length,
                recordsTotal = dto.recordsTotal,
                data = dto.data.Select(d => new UserDto
                {
                    Id = d.Id,
                    LoginName = d.LoginName,
                    RealName = d.RealName,
                    Email = d.Email,
                    CreateDateTime = d.CreateDateTime,
                    Status = d.Status,
                    IsDeleted = d.IsDeleted
                }).ToList()
            };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMyRoles(int moudleId, int menuId, int btnId, int id)
        {
            var query = new RoleRequest
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                OrderBy = Request["orderBy"],
                OrderDir = Request["orderDir"],
                Name = Request["keywords"],
                FacilitatorCode = CurrentUser.FacilitatorCode

            };

            var dto = userService.GetMyRoles(query, id);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetNotMyRoles(int moudleId, int menuId, int btnId, int id)
        {
            var query = new RoleRequest
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                OrderBy = Request["orderBy"],
                OrderDir = Request["orderDir"],
                Name = Request["keywords"],
                FacilitatorCode = CurrentUser.FacilitatorCode

            };
            var dto = userService.GetNotMyRoles(query, id);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AuthenRole(int moudleId, int menuId, int btnId, int id, List<RoleDto> roles)
        {
            var dto = new Result<string>();
            var userRoles = roles.Select(item => new UserRoleDto { UserId = id, RoleId = item.Id, Creater = CurrentUser.Id, IsDeleted = false }).ToList();
            var obj= userRoleService.AddUserRole(userRoles);
            dto.flag = obj.flag;
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UnAuthenRole(string moudleId, string menuId, string btnId, string id, List<RoleDto> roles)
        {
            var dto = new Result<string>();
            var roleIds = roles.Select(item => item.Id).FirstOrDefault();
            var obj = userRoleService.DelUserRole(roleIds, id.ToInt());
            dto.flag = obj.flag;
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public JsonResult Login(UserDto model)
        {
            var result = userService.Login(model);
            //if (result.flag)
            //{
            //    return RedirectToAction("Index", "Control");
            //}
            //ModelState.AddModelError("Error", result.msg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult Add(int moudleId, int menuId, int btnId, UserDto dto)
        {
            dto.Password = dto.Password.ToMD5();
            dto.FacilitatorCode = CurrentUser.FacilitatorCode;
            dto.FacilitatorName = CurrentUser.FacilitatorName;
            dto.Creater = CurrentUser.Id;
            userService.AddUser(dto);
            return RedirectToAction("Index", RouteData.Values);
        }


        [HttpPost]
        public ActionResult Edit(string moudleId, string menuId, string btnId, UserDto dto)
        {
            var old = userService.GetDtail(dto.Id);
            old.Email = dto.Email;
            old.RealName = dto.RealName;
            old.Status = dto.Status;
            old.Modifier = CurrentUser.Id;
            userService.EditUser(old);
            return RedirectToAction("Index", RouteData.Values);
        }


        [HttpPost]
        public JsonResult Delete(int moudleId, int menuId, int btnId, List<int> ids)
        {
            var res = new Result<string>();
            if (ids != null && ids.Any())
            {
                var obj = userService.DelUser(ids);
                res.flag = obj.flag;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult GetTestData()
        {
            List<UserDto> users = new List<UserDto>();
            for (int i = 0; i < 5; i++)
            {
                users.Add(new UserDto()
                {
                    LoginName = "i" + i,
                    Id = i
                });
            }
            return Json(users, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetTest()
        {
            return Json("123", JsonRequestBehavior.AllowGet);
        }
            #endregion
        }
}