using patterns_lab2_2.Interfaces;
using patterns_lab2_2.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.Models.Bridge
{
    public class TextDisplayFormatter: IClanDisplayFormatter
    {
        private StringBuilder _sb = new StringBuilder();

        public void StartReport(Clan c)
        {
            _sb.AppendLine($"===== CLAN REPORT: {c.Name} =====");
        }

        public void StartSquad(Squad s, int squadIndex)
        {
            _sb.AppendLine($"--- Squad {squadIndex + 1} (Count: {s.Persons.Count}) ---");
        }

        public void FormatPlayer(Person p)
        {
            _sb.AppendLine($"  [Player] {p.GetFeatures()} @ ({p.Position.X:F0}, {p.Position.Y:F0})");
        }

        public void FormatLeader(Person p)
        {
            _sb.AppendLine($"  >> [LEADER] {p.GetFeatures()} @ ({p.Position.X:F0}, {p.Position.Y:F0})");
        }

        public string GetResult()
        {
            return _sb.ToString();
        }
    }
}
