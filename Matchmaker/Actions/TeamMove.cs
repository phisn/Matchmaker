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

            TeamColor color = TeamColor.Blue;
            if (!TeamColorExtensions.TryFromString(ref arguments[1]))
            {
                Console.WriteLine("Color not found");
                return false;
            }

            TryRemoveParticipantFromOtherTeam(participant, color);
            Teams.ByColor(color).Participants.Add(participant);

            return true;
        }

        private static void TryRemoveParticipantFromOtherTeam(Participant participant, TeamColor color)
        {
            Teams.ByColor(color.Opposite()).Participants.RemoveAll(
                (p) => p.ParticipantId == participant.ParticipantId);
        }
    }
}
