using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace CacheMonitorLab
{
    public class MemoryCacheMonitor
    {
        public static void SetMonitor()
        {
            string connStr = ConfigurationManager.ConnectionStrings["lab"].ConnectionString;
            CacheItemPolicy policy = new CacheItemPolicy();
            SqlDependency.Start(connStr);
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand command = new SqlCommand("Select MaintenanceMode From dbo.MaintenanceMode", conn))
                {
                    command.Notification = null;
                    SqlDependency dep = new SqlDependency();
                    dep.AddCommandDependency(command);
                    conn.Open();
                    bool inMaintenanceMode = (bool)command.ExecuteScalar();
                    SqlChangeMonitor monitor = new SqlChangeMonitor(dep);
                    policy.ChangeMonitors.Add(monitor);
                    dep.OnChange += Dep_OnChange;
                    MemoryCache.Default.Add("MaintenanceMode", inMaintenanceMode, policy);
                }
            }


        }

        private static void Dep_OnChange(object sender, SqlNotificationEventArgs e)
        {
            //清除相關快取
            List<string> keys = new List<string>();
            var cache = MemoryCache.Default;
            foreach (var item in cache)
            {
                if (item.Key.StartsWith("MaintenanceMode_"))
                    keys.Add(item.Key);
            }
            foreach (var key in keys)
            {
                cache.Remove(key);
            }
            SetMonitor();
        }
    }

}