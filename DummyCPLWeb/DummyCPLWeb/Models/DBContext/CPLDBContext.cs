using DummyCPLWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyCPLWeb.Data
{
    public class CPLDBContext : DbContext
    {
        // 取wwwroot的路徑(DI注入)
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DbSet<CoilSchedule> CoilSchedules { get; set; }

        public CPLDBContext(DbContextOptions options, IWebHostEnvironment webHostEnvironment)
           : base(options)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 創建photo，記得.net core版本需要將Id設值初始化
            var coilSchedules = CreateCoilSchedule();
            modelBuilder.Entity<CoilSchedule>().HasData(coilSchedules);           
        }

        private IEnumerable<CoilSchedule> CreateCoilSchedule()
        {
            return new List<CoilSchedule>
            {
                new CoilSchedule{
                   CoilScheduleID ="HE00010000",
                   SeqNo = 0,
                   UpdateSource = "0",
                   CreateTime = DateTime.Now                 
                },
               new CoilSchedule {
                    CoilScheduleID ="HE00020000",
                   SeqNo = 1,
                   UpdateSource = "0",
                   CreateTime = DateTime.Now
                },
                new CoilSchedule {
                   CoilScheduleID ="HE00030000",
                   SeqNo = 2,
                   UpdateSource = "0",
                   CreateTime = DateTime.Now
                },
                new CoilSchedule {
                   CoilScheduleID ="HE00040000",
                   SeqNo = 3,
                   UpdateSource = "0",
                   CreateTime = DateTime.Now
                }

            };
        }

    }
}
