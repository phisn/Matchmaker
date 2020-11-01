using Matchmaker.Logic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Matchmaker.Actions
{
    public class ShowTeams : Action
    {
        public override string Name => "show teams";
        public override string Description => "prints current teams";
        public override string Usage => "show teams";

        public override bool Call(string[] arguments)
        {
            if (arguments.Length > 0)
            {
                return false;
            }

            Console.WriteLine("Team Blue:");
            PrintTeamParticipants(Teams.Blue);

            Console.WriteLine("\nTeam Red:");
            PrintTeamParticipants(Teams.Red);

            return true;
        }

        private static void PrintTeamParticipants(Team team)
        {
            if (team.Participants.Count > 0)
            {
                for (int i = 0; i < team.Participants.Count; ++i)
                {
                    Console.WriteLine("{0, 2} {1, -20} {2, 5:} score", i, $"{team.Participants[i].Name}#{team.Participants[i].ParticipantId}", team.Participants[i].Score);
                }
            }
            else
            {
                Console.WriteLine("Empty");
            }
        }
    }
}
