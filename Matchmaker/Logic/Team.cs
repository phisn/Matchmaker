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
            return TrueSkillCalculator.CalculateMatchQuality(
                GameInfo.DefaultGameInfo,
                ConvertToMoserware(bluePlayers, redPlayers));
        }

        public static double GetRatingFor(IEnumerable<IDictionary<Player, Rating>> teams)
        {
            return TrueSkillCalculator.CalculateMatchQuality(
                GameInfo.DefaultGameInfo,
                teams);
        }

        public static IEnumerable<IDictionary<Player, Rating>> ConvertToMoserware(List<Participant> redPlayers, List<Participant> bluePlayers)
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

            return Moserware.Skills.Teams.Concat(blueTeam, redTeam);
        }

        public List<int> Participants { get; set; } = new List<int>();
    }
}
