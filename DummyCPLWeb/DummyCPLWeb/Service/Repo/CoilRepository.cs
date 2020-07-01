using DummyCPLWeb.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyCPLWeb.Models.Repo
{
    public class CoilRepository : ICoilRepository
    {
        private readonly CPLDBContext _appDbContext;

        public CoilRepository(CPLDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<CoilSchedule> GetAllCoilSchedule()
        {
            return _appDbContext.CoilSchedules;
        }

        public Task<List<CoilSchedule>> GetAsyAllCoilSchedule()
        {
            return _appDbContext.CoilSchedules.ToListAsync();
        }
    }
}
