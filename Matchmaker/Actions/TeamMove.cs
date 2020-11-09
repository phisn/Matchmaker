using Matchmaker.Logic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Matchmaker.Actions
{
    public class TeamMove : Action
    {
        public override string Name => "team move";
        public override string Description => "moves or adds a participant to a team";
        public override string Usage => "team move <participant/id> <blue/red>";

        public override bool Call(string[] arguments)
        {
            if (arguments.Length != 2)
            {
                return false;
            }

            Participant participant = Participant.FindParticipantByString(arguments[0]);
            if (participant == null)
            {
                Console.WriteLine("Participant not found");
                return false;
            }

            TeamColor? color = TeamColorExtensions.FromString(arguments[1]);

            if (!color.HasValue)
            {
                Console.WriteLine("Color not found");
                return false;
            }


            TryRemoveParticipantFromOtherTeam(participant, color.Value);
            Teams.ByColor(color.Value).Participants.Add(participant.ParticipantId);

            return true;
        }

        private static void TryRemoveParticipantFromOtherTeam(Participant participant, TeamColor color)
        {
            Teams.ByColor(color.Opposite()).Participants.RemoveAll(
                (p) => p == participant.ParticipantId);
        }
    }
}
