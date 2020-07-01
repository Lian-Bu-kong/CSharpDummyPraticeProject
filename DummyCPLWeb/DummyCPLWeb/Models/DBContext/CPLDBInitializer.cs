using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyCPLWeb.Data
{
    public class CPLDBInitializer
    {
        // 將資料庫初始化
        public static void Init(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<CPLDBContext>();

                // 刪除資料庫
                context.Database.EnsureDeleted();

                // 創建料庫
                context.Database.EnsureCreated();

                // 將資料遷移
                context.Database.Migrate();
            }
        }
    }
}
