using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NHibernate.CMS.DTO
{
    /// <summary>
    /// 基仓储实现的方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T : class,new()
    {
        //添加
        object AddEntities(T entity);

        object AddEntities(string entityName, object obj);

        //修改
        bool UpdateEntities(T entity);

        //修改
        bool UpdateEntities(string entityName, object obj);

        //删除
        bool DeleteEntities(T entity);
        //删除
        bool DeleteEntities(string entityName, object obj);
        bool DeleteEntities(string query);
        bool DeleteEntities(string query, object[] values, Type.IType[] types);

        //查询
        IList<T> LoadEntities(Func<T, bool> wherelambda);
        IList<T> LoadEntities(string queryString);

        //分页
        IList<T> LoadPagerEntities<S>(int pageSize, int pageIndex,
            out int total, Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda);
      
        System.Collections.IList ExecuteSQL(string queryString);

        //获取实体
        T GetSingleModel(T entity, object id);

    }
}
