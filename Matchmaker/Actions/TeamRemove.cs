using Matchmaker.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matchmaker.Actions
{
    public class TeamRemove : Action
    {
        public override string Name => "team remove";
        public override string Description => "removes a participant from a team";
        public override string Usage => "team remove <participant/id>";

        public override bool Call(string[] arguments)
        {
            if (arguments.Length != 1)
            {
                return false;
            }

            Participant participant = Participant.FindParticipantByString(arguments[0]);

            if (participant == null)
            {
                Console.WriteLine("Got invalid participant");
                return false;
            }

            if (Teams.Blue.Participants.Remove(participant.ParticipantId))
                return true;

            if (Teams.Red.Participants.Remove(participant.ParticipantId))
                return true;

            Console.WriteLine("Participant not found in teams");
            return true;
        }
    }
}
