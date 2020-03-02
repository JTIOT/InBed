using Memcached.ClientLibrary;

namespace InBed.Core.Memcached
{
    /// <summary>
    /// MemcachedClient单利模式
    /// </summary>
    public class MemcachedClientSingleton : Singleton<MemcachedClient>
    {
        private MemcachedClientSingleton()
        {
            
        }
    }
}
