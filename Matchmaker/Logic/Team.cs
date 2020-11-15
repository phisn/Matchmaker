using Moserware.Skills;
using System;
using System.Collections.Generic;
using System.Text;

namespace Matchmaker.Logic
{
    public class Team
    {
        public static double GetRatingFor(List<Participant> redPlayers, List<Participant> bluePlayers)
        {
            var redTeam = new Moserware.Skills.Team();

            foreach (Participant participant in redPlayers)
            {
                redTeam.AddPlayer(
                    new Player(participant.ParticipantId),
                    new Rating(
                        participant.Mean,
                        participant.StandardDeviation));
            }

            var blueTeam = new Moserware.Skills.Team();

            foreach (Participant participant in bluePlayers)
            {
                blueTeam.AddPlayer(
                    new Player(participant.ParticipantId),
                    new Rating(
                        participant.Mean,
                        participant.StandardDeviation));
            }

            return TrueSkillCalculator.CalculateMatchQuality(
                GameInfo.DefaultGameInfo,
                Moserware.Skills.Teams.Concat(blueTeam, redTeam));
        }

        public List<int> Participants { get; set; } = new List<int>();
    }
}
