using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NHibernate.CMS.Framework.Utility
{
   
    /// <summary>
    /// 线程队列
    /// </summary>
    public class SimpleThreadPoolTasks
    {
        /// <summary>
        /// 线程实体类
        /// </summary>
        private class TaskEntity
        {
            public TaskEntity(Action<object> func, object data)
            {
                this.Function = func;
                this.Data = data;
            }
            public Action<object> Function;
            public object Data;
        }
        /// <summary>
        /// 队列先进先出
        /// </summary>
        private static Queue<TaskEntity> list = new Queue<TaskEntity>();
        /// <summary>
        /// 构造函数
        /// </summary>
        static SimpleThreadPoolTasks()
        {
            Thread th = new Thread(RunTask);
            th.IsBackground = true;//设置为是否后台线程
            th.Start();
        }
        //运行
        private static void RunTask()
        {
            while (true)
            {
                if (list.Count == 0)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    TaskEntity entity;
                    lock (list)
                    {
                        entity = list.Dequeue();//移除
                    }
                    try
                    {
                        entity.Function(entity.Data);
                    }
                    catch { }
                    Thread.Sleep(10);
                }
            }
        }
        /// <summary>
        /// 添加队列
        /// </summary>
        /// <param name="func"></param>
        /// <param name="data"></param>
        public static void Add(Action<object> func, object data)
        {
            lock (list)
            {
                list.Enqueue(new TaskEntity(func, data));//添加队列
            }
        }
    }
}
