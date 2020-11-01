using System;
using System.Collections.Generic;
using System.Text;

namespace Matchmaker.Actions
{
    public class RemoveParticipant : Action
    {
        public override string Name => "remove participant";
        public override string Description => "removes participant from database";
        public override string Usage => "remove participant <name/id>";

        public override bool Call(string[] arguments)
        {
            return false;
        }
    }
}
