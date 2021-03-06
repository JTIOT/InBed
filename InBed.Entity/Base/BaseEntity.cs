﻿using System;

namespace InBed.Entity.Base
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            IsDeleted = false;
            CreateDateTime = DateTime.Now;
        }

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int Creater { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public int? Modifier { get; set; }
    }
}
