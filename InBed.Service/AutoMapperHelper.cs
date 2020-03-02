using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InBed.Entity;
using InBed.Service.Enum;
using InBed.Service.Dto;
using System.Collections;
using System.Data;

namespace InBed.Service
{
    /// <summary>
    /// AutoMapper扩展帮助类
    /// </summary>
    public static class AutoMapperHelper
    {
        /// <summary>
        ///  类型映射
        /// </summary>
        public static T MapTo<T>(this object obj)
        {
            if (obj == null) return default(T);
            Mapper.Map(obj.GetType(), typeof(T));
            return Mapper.Map<T>(obj);
        }
        /// <summary>
        /// 集合列表类型映射
        /// </summary>
        public static List<TDestination> MapToList<TDestination>(this IEnumerable source)
        {
            foreach (var first in source)
            {
                var type = first.GetType();
                Mapper.Map(type, typeof(TDestination));
                break;
            }
            return Mapper.Map<List<TDestination>>(source);
        }
        /// <summary>
        /// 集合列表类型映射
        /// </summary>
        public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            return Mapper.Map<List<TDestination>>(source);
        }
        /// <summary>
        /// 类型映射
        /// </summary>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
            where TSource : class
            where TDestination : class
        {
            if (source == null) return destination;
            return Mapper.Map(source, destination);
        }
        /// <summary>
        /// DataReader映射
        /// </summary>
        public static IEnumerable<T> DataReaderMapTo<T>(this IDataReader reader)
        {
            return Mapper.Map<IDataReader, IEnumerable<T>>(reader);
        }
    }
}
