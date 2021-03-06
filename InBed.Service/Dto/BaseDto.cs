﻿
using System;

namespace InBed.Service.Dto
{
    /// <summary>
    /// DTO基类
    /// </summary>
    public class BaseDto
    {
        public BaseDto()
        {
            CreateDateTime = DateTime.Now;
            IsDeleted = false;
        }
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int Creater { get; set; }

        public int? Modifier { get; set; }

    }
}
