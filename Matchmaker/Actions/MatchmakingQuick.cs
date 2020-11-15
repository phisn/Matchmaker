using Matchmaker.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matchmaker.Actions
{
    public class MatchmakingQuick : Action
    {
        public override string Name => "matchmaking quick";
        public override string Description => "removes all current participants, adds new and shuffels";
        public override string Usage => "matchmaking quick [participants...]";

        public override bool Call(string[] arguments)
        {
            Teams.Blue.Participants.Clear();
            Teams.Red.Participants.Clear();

            using (Context context = new Context())
            {
                foreach (string argument in arguments)
                {
                    Participant participant = Participant.FindParticipantByString(argument);
                    Teams.Blue.Participants.Add(participant.ParticipantId);
                }
            }

            Teams.Optimize();
            return true;
        }
    }
}
