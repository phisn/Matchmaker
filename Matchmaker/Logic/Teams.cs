dded using Moserware.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matchmaker.Logic
{
    public static class Teams
    {
        public static Team Red { get; } = new Team();
        public static Team Blue { get; } = new Team();

        public static Team ByColor(TeamColor color)
            => color == TeamColor.Blue ? Blue : Red;

        public static double GetCurrentTeamRating()
        {
            return Team.GetRatingFor(ConvertToMoserware());
        }

        public static IEnumerable<IDictionary<Player, Rating>> ConvertToMoserware()
        {
            using (Context context = new Context())
            {
                List<Participant> red = Teams.Red.Participants
                    .Select(p => context.Participants.Find(p))
                    .ToList();
                List<Participant> blue = Teams.Blue.Participants
                    .Select(p => context.Participants.Find(p))
                    .ToList();

                return Team.ConvertToMoserware(red, blue);
            }
        }

        public static void Optimize()
        {
            List<KeyValuePair<List<Participant>, List<Participant>>> allTeams = GetAllTeamCombinations();
            allTeams = OrderTeamsByFairness(allTeams);

            KeyValuePair<List<Participant>, List<Participant>> result = SelectTeam(allTeams);

            Teams.Blue.Participants.Clear();
            Teams.Red.Participants.Clear();

            Teams.Blue.Participants.AddRange(result.Key.Select(p => p.ParticipantId));
            Teams.Red.Participants.AddRange(result.Value.Select(p => p.ParticipantId));
        }

        private static List<KeyValuePair<List<Participant>, List<Participant>>> OrderTeamsByFairness(List<KeyValuePair<List<Participant>, List<Participant>>> allTeams)
        {
            Random random = new Random();
            allTeams = allTeams.OrderBy((t) => random.Next()).ToList();

            allTeams = allTeams.OrderBy((t) =>
            {
                return 1.0 - Team.GetRatingFor(t.Key, t.Value);
            }).ToList();
            return allTeams;
        }

        private static List<KeyValuePair<List<Participant>, List<Participant>>> GetAllTeamCombinations()
        {
            List<Participant> participants = GetAllParticipants();

            Participant[] participantsArray = participants.ToArray();
            Participant[][] participantPowerSet = FastPowerSet(participantsArray);

            List<KeyValuePair<List<Participant>, List<Participant>>> allTeams =
                new List<KeyValuePair<List<Participant>, List<Participant>>>();

            foreach (Participant[] team in participantPowerSet)
            {
                KeyValuePair<List<Participant>, List<Participant>> teamPair =
                    new KeyValuePair<List<Participant>, List<Participant>>(
                       new List<Participant>(),
                       new List<Participant>());

                teamPair.Key.AddRange(team);
                teamPair.Value.AddRange(Complement(team, participantsArray));

                allTeams.Add(teamPair);
            }

            return allTeams;
        }

        private static List<Participant> GetAllParticipants()
        {
            List<Participant> participants = new List<Participant>();

            using (Context context = new Context())
            {
                participants.AddRange(Teams.Red.Participants.Select(p => context.Participants.Find(p)));
                participants.AddRange(Teams.Blue.Participants.Select(p => context.Participants.Find(p)));
            }

            return participants;
        }

        private static KeyValuePair<List<Participant>, List<Participant>> SelectTeam(
            List<KeyValuePair<
                List<Participant>,
                List<Participant>>> allTeams)
        {
            Random random = new Random();

            foreach (var pair in allTeams)
            {
                if (random.Next(2) == 0)
                {
                    continue;
                }

                return pair;
            }

            return allTeams.First();
        }

        // https://stackoverflow.com/questions/19890781/creating-a-power-set-of-a-sequence
        private static T[][] FastPowerSet<T>(T[] seq)
        {
            var powerSet = new T[1 << seq.Length][];
            powerSet[0] = new T[0]; // starting only with empty set
            for (int i = 0; i < seq.Length; i++)
            {
                var cur = seq[i];
                int count = 1 << i; // doubling list each time
                for (int j = 0; j < count; j++)
                {
                    var source = powerSet[j];
                    var destination = powerSet[count + j] = new T[source.Length + 1];
                    for (int q = 0; q < source.Length; q++)
                        destination[q] = source[q];
                    destination[source.Length] = cur;
                }
            }
            return powerSet;
        }

        private static T[] Complement<T>(T[] input, T[] overall)
        {
            int index = 0;
            T[] result = new T[overall.Length - input.Length];

            foreach (T value in overall)
                if (!input.Contains(value))
                {
                    result[index++] = value;
                }

            return result;
        }
    }

    public enum TeamColor
    {
        Red,
        Blue
    }

    public static class TeamColorExtensions
    {
        public static TeamColor? FromString(string color)
        {
            switch (color.ToLower().Trim())
            {
                case "red":
                    return TeamColor.Red;
                case "blue":
                    return TeamColor.Blue;
                default:
                    return null;
            }
        }

        public static TeamColor Opposite(this TeamColor teamColor)
        {
            return teamColor == TeamColor.Blue
                ? TeamColor.Red
                : TeamColor.Blue;
        }
    }
}
