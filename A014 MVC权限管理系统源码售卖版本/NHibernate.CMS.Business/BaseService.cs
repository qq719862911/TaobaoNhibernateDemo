using NHibernate.CMS.IDTO;
using NHibernate.CMS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.CMS.Model;

namespace NHibernate.CMS.Business
{
    public abstract class BaseService<T> where T : class,new()
    {
        //在调用这个方法的时候必须给他赋值
        public IDTO.IBaseRepository<T> CurrentRepository { get; set; }
        //基类的构造函数
        public BaseService()
        {
            SetCurrentRepository();  //构造函数里面调用了此设置当前仓储的抽象方法
        }

        //构造方法实现赋值 
        public abstract void SetCurrentRepository();  //约束子类必须实现这个抽象方法
        //添加
        public object AddEntities(T entity)
        {

            return CurrentRepository.AddEntities(entity);
        }
        //添加
        public object AddEntities(string entityName, object obj)
        {

            return CurrentRepository.AddEntities(entityName,obj);
        }

        //修改
        public bool UpdateEntities(T entity)
        {

            return CurrentRepository.UpdateEntities(entity);
        }
        //修改
        public bool UpdateEntities(string entityName, object obj)
        {

            return CurrentRepository.UpdateEntities(entityName,obj);
        }
        //删除
        public bool DeleteEntities(T entity)
        {

            return CurrentRepository.DeleteEntities(entity);
        }
        //删除
        public bool DeleteEntities(string entityName, object obj)
        {

            return CurrentRepository.DeleteEntities(entityName,obj);
        }
        //删除
        public bool DeleteEntities(string query)
        {

            return CurrentRepository.DeleteEntities(query);
        }
        //删除
        public bool DeleteEntities(string query, object[] values, Type.IType[] types)
        {

            return CurrentRepository.DeleteEntities(query,values,types);
        }

        //查询
        public IList<T> LoadEntities(Func<T, bool> wherelambda)
        {
            return CurrentRepository.LoadEntities(wherelambda);

        }
        //查询
        public IList<T> LoadEntities(string queryString)
        {

            return CurrentRepository.LoadEntities(queryString);
        }
        //分页
        public IList<T> LoadPagerEntities<S>(int pageSize, int pageIndex, out int total,
            Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda)
        {

            return CurrentRepository.LoadPagerEntities<S>(pageSize, pageIndex, out total, whereLambda, isAsc, orderByLambda);
        }

        public IList<T> LoadPagerEntities<S>(PageResult pagsinfo, out int total, Func<T, bool> whereLambda, bool isAsc, System.Linq.Expressions.Expression<Func<T, object>> orderByLambda)
        {
            return CurrentRepository.LoadPagerEntities<S>(pagsinfo, out total, whereLambda, isAsc, orderByLambda);
        }
        public IList<T> LoadPagerEntities<S>(PageResult pagsinfo, out int total, string whereLambda, string orderByLambda)
        {
            return CurrentRepository.LoadPagerEntities<S>(pagsinfo, out total, whereLambda, orderByLambda);
        }
        public System.Data.DataTable LoadPagerEntities(PageResult pagsinfo, out int total, string whereLambda, string orderByLambda)
        {
            return CurrentRepository.LoadPagerEntities(pagsinfo, out total, whereLambda, orderByLambda);
        }

        public System.Collections.IList ExecuteSQL(string queryString)
        {


            return CurrentRepository.ExecuteSQL(queryString);

        }
        //获取单条
        public T GetSingleModel(T entity, object id)
        {

            return CurrentRepository.GetSingleModel(entity, id);
        }
        public T GetSingleModel(Func<T, bool> wherelambda)
        {
            return CurrentRepository.GetSingleModel(wherelambda);
        }
    }
}
