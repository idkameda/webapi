using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPISample.Models
{
    public class InputModel
    {
        public class Criteria
        {
            public string CrudType { get; set; }

            public int YearIndex { get; set; }

            public int MonthIndex { get; set; }
        }
    }
}
