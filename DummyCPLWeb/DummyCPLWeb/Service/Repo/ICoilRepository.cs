using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyCPLWeb.Models.Repo
{
    public interface ICoilRepository
    {
        IEnumerable<CoilSchedule> GetAllCoilSchedule();

        Task<List<CoilSchedule>> GetAsyAllCoilSchedule();
    }
}
