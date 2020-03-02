

using System.Data.Entity;

namespace InBed.Data
{
    /// <summary>
    /// 初始化数据
    /// </summary>
    public static class InitData
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<InBedContext, Configuration>());
        }
    }
}
