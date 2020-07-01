using DummyMVCEFModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyMVCEFModel.ViewModel
{
    public class VMDeptEmp
    {
        //建立tDepartment和tEmployee的List物件
        public List<TDepartment> department { get; set; }
        public List<TEmployee> employee { get; set; }

    }
}
