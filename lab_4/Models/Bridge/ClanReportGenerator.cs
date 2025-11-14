using patterns_lab2_2.Interfaces;
using patterns_lab2_2.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.Models.Bridge
{
    public abstract class ClanReportGenerator
    {
        protected readonly IClanDisplayFormatter _formatter;

        public ClanReportGenerator(IClanDisplayFormatter formatter)
        {
            _formatter = formatter;
        }

        public abstract string GenerateReport(Clan c);
    }
}
