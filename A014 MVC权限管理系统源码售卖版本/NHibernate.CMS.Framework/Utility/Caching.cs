﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
 

namespace NHibernate.CMS.Framework.Utility
{
    /// <summary>
    /// 本地缓存帮助类
    /// </summary>
    public class Caching
    {
        /// <summary>
        /// 本地缓存获取
        /// </summary>
        /// <param name="name">key</param>
        /// <returns></returns>
        public static object Get(string name)
        {
            return HttpRuntime.Cache.Get(name);
        }

        /// <summary>
        /// 本地缓存移除
        /// </summary>
        /// <param name="name">key</param>
        public static void Remove(string name)
        {
            if (HttpRuntime.Cache[name] != null)
                HttpRuntime.Cache.Remove(name);
        }

        /// <summary>
        /// 本地缓存写入（默认缓存20min）
        /// </summary>
        /// <param name="name">key</param>
        /// <param name="value">value</param>
        public static void Set(string name, object value)
        {
            Set(name, value, null);
        }

        /// <summary>
        /// 本地缓存写入（默认缓存20min）,依赖项
        /// </summary>
        /// <param name="name">key</param>
        /// <param name="value">value</param>
        /// <param name="cacheDependency">依赖项</param>
        public static void Set(string name, object value, System.Web.Caching.CacheDependency cacheDependency)
        {
            HttpRuntime.Cache.Insert(name, value, cacheDependency, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20));
        }

        /// <summary>
        /// 本地缓存写入
        /// </summary>
        /// <param name="name">key</param>
        /// <param name="value">value</param>
        /// <param name="minutes">缓存分钟</param>
        public static void Set(string name, object value, int minutes)
        {
            HttpRuntime.Cache.Insert(name, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(minutes));
        }

        /// <summary>
        /// 本地缓存写入，包括分钟，是否绝对过期及缓存过期的回调
        /// </summary>
        /// <param name="name">key</param>
        /// <param name="value">value</param>
        /// <param name="minutes"缓存分钟></param>
        /// <param name="isAbsoluteExpiration">是否绝对过期</param>
        /// <param name="onRemoveCallback">缓存过期回调</param>
        public static void Set(string name, object value, int minutes, bool isAbsoluteExpiration, System.Web.Caching.CacheItemRemovedCallback onRemoveCallback)
        {
            if (isAbsoluteExpiration)
                HttpRuntime.Cache.Insert(name, value, null, DateTime.Now.AddMinutes(minutes), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, onRemoveCallback);
            else
                HttpRuntime.Cache.Insert(name, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(minutes), System.Web.Caching.CacheItemPriority.Normal, onRemoveCallback);
        }
    }
}
