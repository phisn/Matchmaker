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
            if (arguments.Length != 1)
            {
                return false;
            }

            Participant participant = Participant.FindParticipantByString(arguments[0]);

            if (participant == null)
            {
                Console.WriteLine("Participant not found");
            }
            else
            {
                Console.WriteLine($"Score: [{participant.Rating}] Mean: [{participant.Mean}] SD: [{participant.StandardDeviation}]");
            }

            return true;
        }
    }
}
