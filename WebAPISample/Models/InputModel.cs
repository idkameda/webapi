using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPISample.Models
{
    public class InputModel
    {
        public class ObjCriteria
        {
            public string CrudType { get; set; }

            public DataTable dtSave { get; set; }
        }
        public class Criteria
        {
            public string CrudType { get; set; }

            public int YearIndex { get; set; } = 2024;

            public int MonthIndex { get; set; } = 1;
        }

        public class SelectCriteria
        {
            public string CrudType { get; set; }

            public string SearchText { get; set; }
        }

        public class CriteriaSave
        {
            public string CrudType { get; set; }
            public string TranDate { get; set; }

            public string BankDesc { get; set; }

            public string BankDesc2 { get; set; }
            public string MyDesc { get; set; }

            public double AmtDeducted { get; set; }

            public string PaidUsing { get; set; }
            public Boolean IsInvestment { get; set; } = false;

            public int FYYear { get; set; }
        }
    }

}
