using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

/**
 RazorPagesMovieContext 會協調 Movie 模型的 EF Core 功能 (建立、更新、刪除等)。 資料內容 (RazorPagesMovieContext) 
 衍生自 Microsoft.EntityFrameworkCore.DbContext。 資料內容會指定資料模型包含哪些實體。
 */

namespace RazorPagesMovie.Data
{
    public class RazorPagesMovieContext : DbContext
    {
        public RazorPagesMovieContext (DbContextOptions<RazorPagesMovieContext> options)
            : base(options)
        {
        }

        public DbSet<RazorPagesMovie.Models.Movie> Movie { get; set; }
    }
}
