using patterns_lab2_2.Interfaces;
using patterns_lab2_2.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.Models.Bridge
{
    public class FullClanReport: ClanReportGenerator
    {
        public FullClanReport(IClanDisplayFormatter formatter) : base(formatter)
        {}

        public override string GenerateReport(Clan c)
        {
            _formatter.StartReport(c);

            for (int i = 0; i < c.Squads.Count; i++)
            {
                var squad = c.Squads[i];

                _formatter.StartSquad(squad, i);

                foreach (var person in squad.Persons)
                {
                    if (person == c.Leader)
                    {
                        _formatter.FormatLeader(person);
                    }
                    else
                    {
                        _formatter.FormatPlayer(person);
                    }
                }
            }

            return _formatter.GetResult();
        }
    }
}
