using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matchmaker.Actions
{
    public class AddParticipant : Action
    {
        public override string Name => "add participant";
        public override string Description => "adds new participant to database";
        public override string Usage => "add participant <name> [score]";

        public override bool Call(string[] arguments)
        {
            if (arguments.Length < 1 || arguments.Length > 2)
            {
                return false;
            }

            int score = 0;
            if (arguments.Length == 2 && !int.TryParse(arguments[1], out score))
            {
                return false;
            }

            using (Context context = new Context())
            {
                if (context.Participants.Where((p) => p.Name == arguments[0]).Any())
                {
                    Console.WriteLine("Participant already exist");
                    return false;
                }

                context.Participants.Add(new Participant
                {
                    Name = arguments[0],
                    Mean = arguments.Length == 2
                        ? score
                        : Participant.DefaultMean,
                    StandardDeviation = arguments.Length == 2
                        ? score / 3.0
                        : Participant.DefaultStandardDeviation,
                    Rating = arguments.Length == 2
                        ? new Moserware.Skills.Rating(
                            score, score / 3.0).ConservativeRating
                        : Participant.DefaultStandardDeviation
                });

                context.SaveChanges();
            }

            return true;
        }
    }
}
