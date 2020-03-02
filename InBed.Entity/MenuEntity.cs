using InBed.Entity.Base;

namespace InBed.Entity
{
    public class MenuEntity : BaseEntity
    {

        /// <summary>
        /// 上级ID
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 排序越大越靠后
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }
    }
}
