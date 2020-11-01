using System;
using System.Collections.Generic;
using System.Text;

namespace Matchmaker.Actions
{
    public class ShowScore : Action
    {
        public override string Name => "show score";
        public override string Description => "shows score for a specific participant";
        public override string Usage => "show score <participant/id>";

        public override bool Call(string[] arguments)
        {
            return false;
        }
    }
}
