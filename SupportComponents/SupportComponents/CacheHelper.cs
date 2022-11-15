using System;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace HS.SupportComponents
{
    public static class CacheHelper
    {
        //2种建立CacheManager的方式
        private static ICacheManager cache = CacheFactory.GetCacheManager();
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="minutes">过期时间</param>
        public static void Add(string key, object value, int minutes)
        {
            cache.Add(key, value, CacheItemPriority.Normal, new MyCacheItemRefreshAction(), new AbsoluteTime(TimeSpan.FromMinutes(minutes)));
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void Add(string key, object value)
        {
            Add(key, value, 120);
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static object GetCache(string key)
        {
            return cache.GetData(key);
        }

        /// <summary>
        /// 移除缓存对象
        /// </summary>
        /// <param name="key">键</param>
        public static void RemoveCache(string key)
        {
            cache.Remove(key);
        }
    }

    /// <summary>
    /// 自定义缓存刷新操作
    /// </summary>
    [Serializable]
    public class MyCacheItemRefreshAction : ICacheItemRefreshAction
    {
        #region ICacheItemRefreshAction 成员
        /// <summary>
        /// 移除过期的缓存
        /// </summary>
        /// <param name="removedKey">移除的键</param>
        /// <param name="expiredValue">过期的值</param>
        /// <param name="removalReason">移除理由</param>
        void ICacheItemRefreshAction.Refresh(string removedKey, object expiredValue, CacheItemRemovedReason removalReason)
        {
            if (removalReason == CacheItemRemovedReason.Expired)
            {
                ICacheManager cache = CacheFactory.GetCacheManager();
                cache.Remove(removedKey);
            }
        }

        #endregion
    }
}
