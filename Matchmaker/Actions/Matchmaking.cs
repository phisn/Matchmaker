using Matchmaker.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matchmaker.Actions
{
    public class Matchmaking : Action
    {
        public override string Name => "matchmaking reload";
        public override string Description => "automatically shuffels fair teams for matches";
        public override string Usage => "matchmaking reload";

        public override bool Call(string[] arguments)
        {
            Teams.Optimize();
            return true;
        }
    }
}
