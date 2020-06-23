using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Hutech.Models
{
    public class SV_SKViewModels
    {
        public SV_SKViewModels()
        {

        }

        public SV_SKViewModels(int id_sv , int id_evens, bool status,DateTime checkin, int id)
        {
            ID_SV = id_sv;
            ID_Events = id_evens;
            Status = status;
            Checkin = checkin;
            ID_Detail_Event_Student = id;
        }
        public Nullable<int> ID_SV { get; set; }
        public Nullable<int> ID_Events { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> Checkin { get; set; }
        public int ID_Detail_Event_Student { get; set; }

      


    }
}