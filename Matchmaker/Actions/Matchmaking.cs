using System;
using System.Collections.Generic;
using System.Text;

namespace Matchmaker.Actions
{
    public class Matchmaking : Action
    {
        public override string Name => "matchmaking";
        public override string Description => "automatically shuffels fair teams for matches";
        public override string Usage => "matchmaking";

        public override bool Call(string[] arguments)
        {
            return false;
        }
    }
}
