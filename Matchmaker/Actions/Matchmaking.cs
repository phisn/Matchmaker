using Matchmaker.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matchmaker.Actions
{
    public class Matchmaking : Action
    {
        public override string Name => "matchmaking";
        public override string Description => "automatically shuffels fair teams for matches";
        public override string Usage => "matchmaking";

        public override bool Call(string[] arguments)
        {
            List<Participant> participants = new List<Participant>();

            using (Context context = new Context())
            {
                participants.AddRange(Teams.Red.Participants.Select(p => context.Participants.Find(p)));
                participants.AddRange(Teams.Blue.Participants.Select(p => context.Participants.Find(p)));
            }

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

            Random random = new Random();
            allTeams = allTeams.OrderBy((t) => random.Next()).ToList();

            allTeams = allTeams.OrderBy((t) =>
            {
                return Math.Abs(t.Key.Sum((p) => p.Rating) - t.Value.Sum((p) => p.Rating));
            }).ToList();

            KeyValuePair<List<Participant>, List<Participant>> result = SelectTeam(allTeams);

            Teams.Blue.Participants.Clear();
            Teams.Red.Participants.Clear();

            Teams.Blue.Participants.AddRange(result.Key.Select(p => p.ParticipantId));
            Teams.Red.Participants.AddRange(result.Value.Select(p => p.ParticipantId));

            return true;
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
        public static T[][] FastPowerSet<T>(T[] seq)
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

        public static T[] Complement<T>(T[] input, T[] overall)
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
}
