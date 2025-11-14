using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.Commands
{
    public class SquadCommand
    {
        public CommandType Type { get; }

        public SquadCommand(CommandType type)
        {
            Type = type;
        }
    }
}
