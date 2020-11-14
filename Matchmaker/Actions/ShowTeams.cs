using Matchmaker.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
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

            double redScore = Teams.GetCurrentScoreForRed();
            Console.WriteLine($"Team Blue: (win { (redScore * 100):F1}%)");
            PrintTeamParticipants(Teams.Blue);

            Console.WriteLine($"\nTeam Red: (win { ((1 - redScore) * 100):F1}%)");
            PrintTeamParticipants(Teams.Red);

            return true;
        }

        private static void PrintTeamParticipants(Team team)
        {
            if (team.Participants.Count > 0)
            {
                using (Context context = new Context())
                {
                    for (int i = 0; i < team.Participants.Count; ++i)
                    {
                        Participant p = context.Participants.Find(team.Participants[i]);
                        Console.WriteLine("{0, 2} {1, -20} {2, 5:} score", i, $"{p.Name}#{p.ParticipantId}", p.Rating);
                    }
                }
            }
            else
            {
                Console.WriteLine("Empty");
            }
        }
    }
}
