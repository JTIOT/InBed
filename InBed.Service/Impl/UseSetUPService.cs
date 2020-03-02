using InBed.Core;
using InBed.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InBed.Service.Dto;
using InBed.Data.DAL;
using InBed.Service.Enum;
using InBed.Service.Request;
using InBed.Core.Extentions;
using InBed.Entity;

namespace InBed.Service.Impl
{
    public class UseSetUPService : IDependency, IUseSetUPService
    {
        public Result<SystemElderSetupDto> AddUseSet(SystemElderSetupDto model)
        {
            Result<SystemElderSetupDto> res = new Result<SystemElderSetupDto>();
            try
            {
                var entity = new ElderSetupEntity {
                    CreateDateTime = DateTime.Now,
                    Creater = model.Creater,
                    Elder_id = model.ElderID,
                    Facilitator_Code = model.FacilitatorCode,
                    Facilitator_Name = model.FacilitatorName,
                    is_enable = 1,
                    start_data = DateTime.Now,
                    end_data = DateTime.Now.AddDays(30)
                };
                var falg = new UseSetUPDAL().AddElderSetup(entity);
                if (falg)
                {
                    res.flag = true;
                    res.msg = "添加成功!";
                }
                else
                {
                    res.flag = false;
                    res.msg = "添加失败!";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = "添加失败!";
            }
            return res;
        }

        public Result<SystemElderSetupDto> DelUseSet(List<int> ids)
        {
            Result<SystemElderSetupDto> res = new Result<SystemElderSetupDto>();
            try {
                var falg= new UseSetUPDAL().DelElderSetup(ids);
                if (falg)
                {
                    res.flag = true;
                    res.msg = "删除成功!";
                }
                else
                {
                    res.flag = false;
                    res.msg = "删除失败!";
                        
                }

            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = "删除失败!";
            }
            return res;
        }

        public Result<SystemElderSetupDto> EditUseSet(SystemElderSetupDto model)
        {
            Result<SystemElderSetupDto> res = new Result<SystemElderSetupDto>();
            try {
                var ElderSetupEntity = new ElderSetupEntity {
                    Elder_id = model.ElderID,
                    Id = model.Id,
                    is_enable = model.is_enable == true ? 1 : 0,
                    Modifier = model.Modifier
                };
                try {
                    var s_date = DateTime.Parse(model.start_data);
                    var e_date = DateTime.Parse(model.end_data);
                    ElderSetupEntity.start_data = s_date;
                    ElderSetupEntity.end_data = e_date;
                } catch
                {
                    ElderSetupEntity.start_data = DateTime.MaxValue;
                    ElderSetupEntity.end_data = DateTime.MaxValue;

                }
                var falg = new UseSetUPDAL().EditElderSetup(ElderSetupEntity);
                if (falg)
                {
                    res.flag = true;
                    res.msg = "修改成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = "修改失败！";
                }



            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = "修改失败！";
            }
            return res;
        }

        public SystemElderSetupDto GetDtail(int id)
        {
            SystemElderSetupDto res = new SystemElderSetupDto();
            try {
                var entity = new UseSetUPDAL().GetDetail(id);
                if (entity != null)
                {
                    res.Id = entity.Id;
                    res.ElderID = (int)entity.Elder_id;
                    res.start_data = entity.start_data.ToShortDateString();
                    res.end_data = entity.end_data.ToShortDateString();
                    res.is_enable =entity.is_enable==0?false:true;
                    res.FacilitatorCode = entity.Facilitator_Code;
                    res.FacilitatorName = entity.Facilitator_Name;
                    var elderEntity = new ElderDAL().Details((int)entity.Elder_id);
                    if (elderEntity != null)
                        res.ElderName = elderEntity.name;

                }
            }
            catch (Exception ex)
            {
                return new SystemElderSetupDto();
            }
            return res;
        }

        public ResultDto<UseSetUPDto> GetWithPages(UseSetUPRequest request)
        {
            ResultDto<UseSetUPDto> res = new ResultDto<UseSetUPDto>();
            try {
                int count = 0;
                var elderEntity = new ElderDAL().Query(request.FacilitatorCode);
                if (!StringExtension.IsBlank(request.ElderName) || !StringExtension.IsBlank(request.ElderPhone))
                {
                    var itemEntity = elderEntity.FindAll(p=>p.name== request.ElderName|| p.homephone== request.ElderPhone);
                    if (itemEntity != null)
                    {
                        var fastEntity = itemEntity.FirstOrDefault();
                        var entity = new UseSetUPDAL().GetWithPages(request.Start, request.Length, request.FacilitatorCode, fastEntity.Id, request.StartTime, request.EndTime, request.OrderBy, out count, request.OrderDir);
                        if (entity != null)
                        {

                            var r_data = toDTO(entity);
                            foreach (var item in r_data)
                            {
                                var tempEntity = elderEntity.Find(p => p.Id == item.ElderID);
                                if (tempEntity != null)
                                {
                                    item.ElderName = tempEntity.name;
                                    item.ElderPhone = tempEntity.homephone;
                                    item.birthday = tempEntity.birthday;
                                }
                                else
                                {
                                    r_data.Remove(item);
                                }
                            }
                            res.data = r_data;

                        }
                        
                    }
                }
                else
                {
                    var entity = new UseSetUPDAL().GetWithPages(request.Start, request.Length, request.FacilitatorCode, 0, request.StartTime, request.EndTime, request.OrderBy, out count, request.OrderDir);
                    if (entity != null)
                    {
                        var r_data = toDTO(entity);
                        foreach (var item in r_data)
                        {
                            var tempEntity = elderEntity.Find(p =>p.Id== item.ElderID);
                            if (tempEntity != null)
                            {
                                item.ElderName = tempEntity.name;
                                item.ElderPhone = tempEntity.homephone;
                                item.birthday = tempEntity.birthday;
                            }
                            else {
                                r_data.Remove(item);
                            }
                        }
                        res.data = r_data;
                    }
                }
               
                
            }
            catch (Exception ex)
            {

            }
            return res;
        }

        public List<UseSetUPDto> toDTO(List<Entity.ElderSetupEntity> data)
        {
            List<UseSetUPDto> res = new List<UseSetUPDto>();
            data.ForEach(p => res.Add(new UseSetUPDto {
                ElderID = (int)p.Elder_id,
                Id = p.Id,
                end_data = p.end_data.ToString(),
                start_data = p.start_data.ToString()

            }));
            return res;
        }
    }
}
