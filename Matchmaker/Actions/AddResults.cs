using System;
using System.Collections.Generic;
using System.Text;

namespace Matchmaker.Actions
{
    public class AddResults : Action
    {
        public override string Name => "add results";
        public override string Description => "asks for results and updates scores for both teams";
        public override string Usage => "add results <winner: blue/red>";

        public override bool Call(string[] arguments)
        {
            return false;
        }
    }
}
