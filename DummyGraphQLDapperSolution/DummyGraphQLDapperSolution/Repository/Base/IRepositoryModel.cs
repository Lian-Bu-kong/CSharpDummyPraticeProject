using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyGraphQLDapperSolution.Repository
{
    public interface IRepositoryModel
    {
        /// <summary>
        /// 流水序號ID，最小值為1
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// 此筆記錄最後變更時間
        /// </summary>
        DateTime UpdateTime { get; set; }

        /// <summary>
        /// 此筆記錄新增時間
        /// </summary>
        DateTime CreateTime { get; set; }
    }
}
