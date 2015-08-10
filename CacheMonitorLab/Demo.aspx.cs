using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CacheMonitorLab
{
    public partial class Demo : System.Web.UI.Page
    {
        ObjectCache cache = MemoryCache.Default;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                return;
            }
            MemoryCacheMonitor.SetMonitor();//ps:可以放在global.asax作初始動作
            Show();
        }

        private void Show()
        {
            lblCaches.Text = "";
            foreach (var item in cache)
                lblCaches.Text += string.Format("Key:{0},Value:{1} +<br/>", item.Key, item.Value);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string key = string.Format("MaintenanceMode_{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
            cache.Add(key, DateTime.Now, null);
            Show();
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            Show();
        }

        protected void btnNotify_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["lab"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("update MaintenanceMode set MaintenanceMode=IIF(MaintenanceMode=0,1,0);", conn))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}