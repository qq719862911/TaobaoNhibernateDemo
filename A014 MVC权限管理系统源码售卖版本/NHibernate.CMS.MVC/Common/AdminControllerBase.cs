
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NHibernate.CMS.MVC.Common
{
    [Permission]
    public class AdminControllerBase : Controller
    {
        
         #region 分页信息
		  
         /// <summary>
        /// 得到当前页
         /// </summary>
        protected int GetPageIndex
         {
             get
             {
                 string pags = "";
                 if (HttpContext.Request.QueryString["page"] == null)
                 { pags = HttpContext.Request["page"]; }
                 else
                 { pags = HttpContext.Request.QueryString["page"]; }
                 if (string.IsNullOrWhiteSpace(pags))
                 {
                     return 1;
                 }
                 return int.Parse(pags);
             }
             
         }
         /// <summary>
         /// 得到每页显示多少
         /// </summary>
         protected  int GetPageSize
         {
             get
             {
                 string rp = "";
                 if (HttpContext.Request.QueryString["rp"] == null)
                 { rp = HttpContext.Request["rp"]; }
                 else
                 { rp = HttpContext.Request.QueryString["rp"]; }
                 if (string.IsNullOrWhiteSpace(rp))
                 {
                     return 10;
                 }
                 return int.Parse(rp);
             }
              
         }
         /// <summary>
         /// 得到排序字段
         /// </summary>
         protected string GetSort
         {
             get
             {
                
                 if (HttpContext.Request.QueryString["sortname"] == null)
                 { return HttpContext.Request["sortname"]; }
                 else
                 { return HttpContext.Request.QueryString["sortname"]; }
                 
             }
              
         }
         /// <summary>
         /// 得到排序方式 asc/desc
         /// </summary>
         protected bool GetOrderBy
         {
             get
             {
                 string order = "asc";
                 if (HttpContext.Request.QueryString["sortorder"] == null)
                 { order= HttpContext.Request["sortorder"]; }
                 else
                 { order= HttpContext.Request.QueryString["sortorder"]; }
                 if (order == "asc")
                     return false;
                 else
                     return true;

             }
              
         }

        /// <summary>
        /// 字段 +排序
        /// </summary>
         protected string GetSotrOrOrderBy {
             get { 
               string order = "asc";
                 if (HttpContext.Request.QueryString["sortorder"] == null)
                 { order= HttpContext.Request["sortorder"]; }
                 else
                 { order= HttpContext.Request.QueryString["sortorder"]; }

                 return GetSort + " " + order;
             }
         }

         /// <summary>
         /// 得到要过滤的字段
         /// </summary>
         protected string GetFilterField
         {
             get
             {

                 if (HttpContext.Request.QueryString["qtype"] == null)
                 { return HttpContext.Request["qtype"]; }
                 else
                 { return HttpContext.Request.QueryString["qtype"]; }

             }
              
         }

         /// <summary>
         /// 得到要过滤的内容
         /// </summary>
         protected string GetLetterPressed
         {
             get
             {

                 if (HttpContext.Request.QueryString["letter_pressed"] == null)
                 { return HttpContext.Request["letter_pressed"]; }
                 else
                 { return HttpContext.Request.QueryString["letter_pressed"]; }

             }
              
         }
         /// <summary>
         /// 查询条件
         /// </summary>
         protected string GetQueryStr
         { 
           get
             {

                 if (HttpContext.Request.QueryString["query"] == null)
                 { return HttpContext.Request["query"]; }
                 else
                 { return HttpContext.Request.QueryString["query"]; }

             }
         }
         #endregion

        #region 弹出框信息
         #region JS提示============================================
         /// <summary>
         /// 添加编辑删除提示
         /// </summary>
         /// <param name="msgtitle">提示文字</param>
         /// <param name="isDele">是否删除</param>
         /// <param name="msgcss">CSS样式</param>
         protected ContentResult JscriptMsg(string msgtitle,bool isDele,  string msgcss)
         {
             string urs = isDele == true ? "" : "1";
             string msbox = "<script> parent.jsprint(\"" + msgtitle + "\", \"" + urs + "\", \"" + msgcss + "\");</script>";
            // ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
             return this.Content(msbox);
         }
         /// <summary>
         /// 带回传函数的添加编辑删除提示
         /// </summary>
         /// <param name="msgtitle">提示文字</param>
         /// <param name="url">返回地址</param>
         /// <param name="msgcss">CSS样式</param>
         /// <param name="callback">JS回调函数</param>
         protected ContentResult JscriptMsg(string msgtitle, string url, string msgcss, string callback)
         {
             string msbox = "<script> parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\", " + callback + ");</script>";
             
             //var script = string.Format("<script>{0}; parent.location.reload(1)</script>", string.IsNullOrEmpty(alert) ? string.Empty : "alert('" + alert + "')");
             return this.Content(msbox);
         }

         #endregion
         /// <summary>
         /// 当弹出DIV弹窗时，需要刷新浏览器整个页面
         /// </summary>
         /// <returns></returns>
         public ContentResult RefreshParent(string alert = null)
         {
             //string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\")";
             var script = string.Format("<script>{0}; parent.location.reload(1)</script>", string.IsNullOrEmpty(alert) ? string.Empty : "alert('" + alert + "')");
             return this.Content(script);
         }

         public new ContentResult RefreshParentTab(string alert = null)
         {
             var script = string.Format("<script>{0}; if (window.opener != null) {{ window.opener.location.reload(); window.opener = null;window.open('', '_self', '');  window.close()}} else {{parent.location.reload(1)}}</script>", string.IsNullOrEmpty(alert) ? string.Empty : "alert('" + alert + "')");
             return this.Content(script);
         }

         /// <summary>
         /// 用JS关闭弹窗
         /// </summary>
         /// <returns></returns>
         public ContentResult CloseThickbox()
         {
             return this.Content("<script>top.tb_remove()</script>");
         }

         /// <summary>
         ///  警告并且历史返回
         /// </summary>
         /// <param name="notice"></param>
         /// <returns></returns>
         public ContentResult Back(string notice)
         {
             var content = new StringBuilder("<script>");
             if (!string.IsNullOrEmpty(notice))
                 content.AppendFormat("alert('{0}');", notice);
             content.Append("history.go(-1)</script>");
             return this.Content(content.ToString());
         }


         public ContentResult PageReturn(string msg, string url = null)
         {
             var content = new StringBuilder("<script type='text/javascript'>");
             if (!string.IsNullOrEmpty(msg))
                 content.AppendFormat("alert('{0}');", msg);
             if (string.IsNullOrWhiteSpace(url))
                 url = Request.Url.ToString();
             content.Append("window.location.href='" + url + "'</script>");
             return this.Content(content.ToString());
         }

         /// <summary>
         /// 转向到一个提示页面，然后自动返回指定的页面
         /// </summary>
         /// <param name="notice"></param>
         /// <param name="redirect"></param>
         /// <returns></returns>
         public ContentResult Stop(string notice, string redirect, bool isAlert = false)
         {
             var content = "<meta http-equiv='refresh' content='1;url=" + redirect + "' /><body style='margin-top:0px;color:red;font-size:24px;'>" + notice + "</body>";

             if (isAlert)
                 content = string.Format("<script>alert('{0}'); window.location.href='{1}'</script>", notice, redirect);

             return this.Content(content);
         }
        #endregion

    } 
}