using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DummyCPLWeb.Models
{
    public class CoilSchedule
    {
        //[Key]
        //public int ID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("鋼捲編號")]
        public string CoilScheduleID { get; set; }
        [DisplayName("排序號")]
        public short SeqNo { get; set; }
        [DisplayName("更新來源")]
        public string UpdateSource { get; set; }
        [DisplayName("建立時間")]
        public DateTime CreateTime { get; set; }
    }
}
