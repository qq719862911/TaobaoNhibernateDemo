using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.CMS.Framework.Utility
{
   public class Utils
    {
        #region 操作权限菜单
        /// <summary>
        /// 获取操作权限
        /// </summary>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, string> ActionType()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Show", "显示");
            dic.Add("List", "查看");
            dic.Add("Add", "添加");
            dic.Add("Edit", "修改");
            dic.Add("Delete", "删除");
            dic.Add("Authorize", "授权");
            dic.Add("Audit", "审批");
            dic.Add("ExaminApproval", "审核");
            dic.Add("Reply", "回复");
            dic.Add("Confirm", "确认");
            dic.Add("Cancel", "取消");
            dic.Add("Invalid", "作废");
            dic.Add("Build", "生成");
            dic.Add("Instal", "安装");
            dic.Add("Unload", "卸载"); 
            dic.Add("Back", "备份");
            dic.Add("Restore", "还原");
            dic.Add("Replace", "替换");
            dic.Add("Quality", "质检");
            dic.Add("ReturnGoods", "退货");
            dic.Add("Complete", "完成");
            dic.Add("Print", "打印");
            dic.Add("Payment", "付款");
            dic.Add("Statement", "结单");
            return dic;
        }
        #endregion
    }
}
