using Matchmaker.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

namespace Matchmaker.Actions
{
    public class SearchParticipant : Action
    {
        public override string Name => "search participant";
        public override string Description => "search participant from database";
        public override string Usage => "search participant <name/id/*>";

        public override bool Call(string[] arguments)
        {
            if (arguments.Length != 1)
            {
                return false;
            }

            Participant participant = Participant.FindParticipantByString(arguments[0]);
            if (participant == null)
            {
                List<Participant> participants;
                
                using (Context context = new Context())
                {
                    if (arguments[0] == "*") 
                    {
                        participants = context.Participants.ToList();
                    }
                    else
                    {
                        participants = context.Participants
                            .Where(p => p.Name.Contains(arguments[0]))
                            .ToList();
                    }
                }

                if (participants.Count > 0)
                {
                    Console.WriteLine($"{participants.Count} participants found: ");
                    for (int i = 0; i < participants.Count; ++i)
                    {
                        Console.WriteLine($"{i,3}: {$"{participants[i].Name}#{participants[i].ParticipantId}", -15} {$"Score: [{participants[i].Rating, 5:F2}] Mean: [{participants[i].Mean, 5:F2}] SD: [{participants[i].StandardDeviation, 5:F2}]"}");

                    }
                }
                else
                {
                    Console.WriteLine("No participants found");
                }
            }
            else
            {
                Console.WriteLine($"Participant found:");
                Console.WriteLine($"{$"{participant.Name}#{participant.ParticipantId}", -15} {$"Score: [{participant.Rating, 5:F2}] Mean: [{participant.Mean, 5:F2}] SD: [{participant.StandardDeviation, 5:F2}]"}");
            }

            return true;
        }
    }
}
