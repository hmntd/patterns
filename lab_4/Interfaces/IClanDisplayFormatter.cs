using patterns_lab2_2.Models.Persons;
using patterns_lab2_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.Interfaces
{
    public interface IClanDisplayFormatter
    {
        void StartReport(Clan c);
        void StartSquad(Squad s, int squadIndex);
        void FormatPlayer(Person p);
        void FormatLeader(Person p);
        string GetResult();
    }
}
