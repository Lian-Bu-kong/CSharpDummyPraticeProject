using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DummyGraphQLDapperSolution.DB.DBAttributes;

namespace DummyGraphQLDapperSolution.Repository
{
    public class BaseRepositoryModel : IRepositoryModel
    {
        [IgnoreReflction]
        public virtual string Id { get; set; }                //選擇性實作
        [IgnoreReflction]
        public virtual DateTime UpdateTime { get; set; }    //選擇性實作
        [IgnoreReflction]
        public virtual DateTime CreateTime { get; set; }    //選擇性實作
    }
}
