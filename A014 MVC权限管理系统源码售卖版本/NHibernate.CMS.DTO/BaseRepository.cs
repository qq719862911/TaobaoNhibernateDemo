using NHibernate.Cfg;
using NHibernate;
using NHibernate.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.CMS.Framework.Web;
using System.Data;
using System.Collections;
using NHibernate.Engine;
using NHibernate.CMS.Model;
using NHibernate.Transform;
using NHibernate.Criterion.Lambda;

namespace NHibernate.CMS.DTO
{
    //连接-单例模式 用法2
    public class Singleton
    {
        private  static Singleton _instance = null;
        private static readonly object lockHelper = new object();

        protected  ISession m_Session;
        public ISession SingletonSession
        {
            get { return m_Session; }
        }

        protected  ISessionFactory Singleton_SessionFactory;
        private Singleton() {
            // string path = NHibernate.CMS.Framework.Utility.AppSettingsHelper.GetString("hibernatecfgxml") + "Config/hibernate.cfg.xml";
            string path = HttpContextBase.GetServerPath("Config/hibernate.cfg.xml");
            var config = new Configuration().Configure(path);
            Singleton_SessionFactory = config.BuildSessionFactory();
            m_Session = Singleton_SessionFactory.OpenSession();
        }
        public static Singleton CreateInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockHelper)
                    {
                        if (_instance == null)
                            _instance = new Singleton();
                    }
                }
                return _instance;
            }
        }

         
    }
    public partial class BaseRepository<T> where T : class
    {


        //添加
        public object AddEntities(T entity)
        {

            try
            {

                var id = Singleton.CreateInstance.SingletonSession.Save(entity);
                Singleton.CreateInstance.SingletonSession.Flush();
                return id;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return null;
            
        }
        //添加
        public object AddEntities(string entityName, object obj)
        {
            try
            {

                var id = Singleton.CreateInstance.SingletonSession.Save(entityName, obj);
                Singleton.CreateInstance.SingletonSession.Flush();
                return id;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return null;
            
        }

        //修改
        public bool UpdateEntities(T entity)
        {
            try
            {
                Singleton.CreateInstance.SingletonSession.Update(entity);
                Singleton.CreateInstance.SingletonSession.Flush();
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            
            return false;
        }
        //修改
        public bool UpdateEntities(string entityName, object obj)
        {
            try
            {
                Singleton.CreateInstance.SingletonSession.Update(entityName, obj);
                Singleton.CreateInstance.SingletonSession.Flush();
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            
            return false;
        }
        //删除
        public bool DeleteEntities(T entity)
        {
            try
            {
                Singleton.CreateInstance.SingletonSession.Delete(entity);
                Singleton.CreateInstance.SingletonSession.Flush();
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            
            return false;
        }
        //删除
        public bool DeleteEntities(string entityName, object obj)
        {
            try
            {
                Singleton.CreateInstance.SingletonSession.Delete(entityName, obj);
                Singleton.CreateInstance.SingletonSession.Flush();
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }
        //删除
        public bool DeleteEntities(string query)
        {
            try
            {
                Singleton.CreateInstance.SingletonSession.Delete(query);
                
                Singleton.CreateInstance.SingletonSession.Flush();
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }
        //删除
        public bool DeleteEntities(string query, object[] values, Type.IType[] types)
        {
            try
            {
                Singleton.CreateInstance.SingletonSession.Delete(query, values, types);
                Singleton.CreateInstance.SingletonSession.Flush();
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }

        //查询
        public IList<T> LoadEntities(Func<T, bool> wherelambda)
        {
            return Singleton.CreateInstance.SingletonSession.Query<T>() .Where(wherelambda).ToList<T>();
             
        }
        //查询
        public IList<T> LoadEntities(string queryString)
        {
            IQuery query = Singleton.CreateInstance.SingletonSession.CreateQuery(queryString);
            return query.List<T>();
        }
        //分页
        public IList<T> LoadPagerEntities<S>(int pageSize, int pageIndex, out int total,
            Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda)
        {
            var tempData = Singleton.CreateInstance.SingletonSession.Query<T>().Where<T>(whereLambda);
             
            total = tempData.Count();
            //排序获取当前页的数据
            if (isAsc)
            {
                tempData = tempData.OrderBy<T, S>(orderByLambda).
                      Skip<T>(pageSize * (pageIndex - 1)).
                      Take<T>(pageSize).ToList();
            }
            else
            {
                tempData = tempData.OrderByDescending<T, S>(orderByLambda).
                     Skip<T>(pageSize * (pageIndex - 1)).
                     Take<T>(pageSize).ToList();
            }
            return tempData.ToList();
        }
        ////分页 System.Linq.Expressions.Expression<Func<T, bool>>
        public IList<T> LoadPagerEntities<S>(PageResult pagsinfo, out int total, Func<T, bool> whereLambda, bool isAsc, System.Linq.Expressions.Expression<Func<T, object>> orderByLambda)
        {
            //检查查询变量
            if (pagsinfo.pageIndex < 0)
                throw new ArgumentException("当前页数不能小于0", "pageIndex");

            if (pagsinfo.pageSize <= 0)
                throw new ArgumentException("每页记录数不能小于0", "pageCount");
             
            int skip, take;
         
            skip =  pagsinfo.pageSize*(pagsinfo.pageIndex - 1) ;
            take = pagsinfo.pageSize;
            
           
            var queryOver = Singleton.CreateInstance.SingletonSession.Query<T>().Where(whereLambda);
            var Ovorder = Singleton.CreateInstance.SingletonSession.Query<T>().Where(whereLambda);
            total = Ovorder.ToList().Count;
            if (isAsc)
                
                return queryOver.AsQueryable().OrderBy(orderByLambda).Skip(skip).Take(take).ToList();
            
            else
                return queryOver.AsQueryable().OrderByDescending(orderByLambda).Skip(skip).Take(take).ToList();

        }

        /// <summary>
        /// 执行sql分页
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="pagsinfo"></param>
        /// <param name="total"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderByLambda"></param>
        /// <returns></returns>
        public IList<T> LoadPagerEntities<S>(PageResult pagsinfo, out int total, string whereLambda, string orderByLambda)
        {
             if (pagsinfo.pageIndex < 0)
                throw new ArgumentException("当前页数不能小于0", "pageIndex");

            if (pagsinfo.pageSize <= 0)
                throw new ArgumentException("每页记录数不能小于0", "pageCount");
            
            if(string.IsNullOrWhiteSpace(whereLambda))
                whereLambda=" 1=1 ";

            int skip, take;
            //(@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize 
            skip =  (pagsinfo.pageSize+1)*(pagsinfo.pageIndex - 1);
            take = (pagsinfo.pageSize*pagsinfo.pageIndex);
            string queryString1=string.Format("select ROW_NUMBER() OVER( ORDER BY  {0}) AS RowNumber,* from {1} where {2} ",orderByLambda, typeof(T).Name, whereLambda);
            string queryString =string.Format( @"select * 
from( 
{0}
) T where RowNumber BETWEEN {1} and {2} ", queryString1, skip, take);
            
            ISQLQuery query1=Singleton.CreateInstance.SingletonSession.CreateSQLQuery(queryString1);
            total = query1.List().Count;
            //这种需要实体持久化
            IQuery query = Singleton.CreateInstance.SingletonSession.CreateSQLQuery(queryString).AddEntity(typeof(T).Name);

            return query.List<T>();
              
        }
      
        public DataTable LoadPagerEntities(PageResult pagsinfo, out int total, string whereLambda, string orderByLambda)
        {
            if (pagsinfo.pageIndex < 0)
                throw new ArgumentException("当前页数不能小于0", "pageIndex");

            if (pagsinfo.pageSize <= 0)
                throw new ArgumentException("每页记录数不能小于0", "pageCount");

            if (string.IsNullOrWhiteSpace(whereLambda))
                whereLambda = " 1=1 ";

            int skip, take;
            //(@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize 
            skip = (pagsinfo.pageSize + 1) * (pagsinfo.pageIndex - 1);
            take = (pagsinfo.pageSize * pagsinfo.pageIndex);
            string queryString1 = string.Format("select ROW_NUMBER() OVER( ORDER BY  {0}) AS RowNumber,* from {1} where {2} ", orderByLambda, typeof(T).Name, whereLambda);
            string queryString = string.Format(@"select * 
from( 
{0}
) T where RowNumber BETWEEN {1} and {2} ", queryString1, skip, take);
             
                ISQLQuery query1 = Singleton.CreateInstance.SingletonSession.CreateSQLQuery(queryString1);
                total = query1.List().Count;
                using (IDbCommand command = Singleton.CreateInstance.SingletonSession.Connection.CreateCommand())
                {
                    command.CommandText = queryString;

                    IDataReader reader = command.ExecuteReader();
                    DataTable result = new DataTable();
                     result.Load(reader);//此方法亦可
                     return result;
                   // return reader.GetSchemaTable();
                } 
        }


        public IList ExecuteSQL(string queryString)
        {
            
            ISQLQuery query = Singleton.CreateInstance.SingletonSession.CreateSQLQuery(queryString);
            return query.List();
             
        }
        //获取单条
        public T GetSingleModel(T entity, object id)
        {
            System.Type types = typeof(T);
          object obj=  Singleton.CreateInstance.SingletonSession.Get(types.Name, id);
          if (obj == null) return null;
          return obj as T;
        }
        //获取单条
        public T GetSingleModel(Func<T, bool> wherelambda)
        {
            System.Type types = typeof(T);
            var obj = Singleton.CreateInstance.SingletonSession.Query<T>().Where(wherelambda).ToList<T>().FirstOrDefault();
            if (obj == null) return null;
            return obj as T;
        }
    }
}
