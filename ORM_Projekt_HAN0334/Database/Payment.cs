using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Projekt_HAN0334.ORM
{
    public class Payment
    {
        public int IdPayment { get; set; }
        public float Cost { get; set; }
        public DateTime PayDate { get; set; }
        public DateTime PayToDate { get; set; }
        public int IdUser { get; set; }
    }
}
