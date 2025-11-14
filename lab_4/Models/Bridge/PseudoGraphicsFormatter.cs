using patterns_lab2_2.Interfaces;
using patterns_lab2_2.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.Models.Bridge
{
    public class PseudoGraphicsFormatter: IClanDisplayFormatter
    {
        private StringBuilder _sb = new StringBuilder();

        public void StartReport(Clan c)
        {
            _sb.AppendLine($"*** {c.Name.ToUpper()} ***");
            _sb.AppendLine("**********************");
        }

        public void StartSquad(Squad s, int squadIndex)
        {
            _sb.AppendLine($"  [Group {squadIndex + 1}]");
        }

        public void FormatPlayer(Person p)
        {
            _sb.AppendLine($"    o  ({p.Position.X:F0}, {p.Position.Y:F0})");
        }

        public void FormatLeader(Person p)
        {
            _sb.AppendLine($"    👑  ({p.Position.X:F0}, {p.Position.Y:F0}) [LEADER]");
        }

        public string GetResult()
        {
            return _sb.ToString();
        }
    }
}
