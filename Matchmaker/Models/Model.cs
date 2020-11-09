using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matchmaker
{
    public class Context : DbContext
    {
        public Context()
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=save.db");

        public DbSet<Participant> Participants { get; set; }
    }

    public class Participant
    {
        public static double DefaultMean { get; } = 25.0;
        public static double DefaultStandardDeviation { get; } = 25.0 / 3.0;

        public Participant()
        {
            Mean = DefaultMean;
            StandardDeviation = DefaultStandardDeviation;
            Rating = 0;
        }

        public int ParticipantId { get; set; }
        public string Name { get; set; }

        public double Mean { get; set; }
        public double StandardDeviation { get; set; }
        public double Rating { get; set; }

        public static Participant FindParticipantByString(string participantName)
        {
            using (Context context = new Context())
            {
                Participant participant = context.Participants
                    .Where((p) => p.Name == participantName.ToLower())
                    .FirstOrDefault();

                if (participant != null)
                    return participant;

                int participantID;
                if (int.TryParse(participantName, out participantID))
                {
                    return context.Participants.Find(participantID);
                }

                return null;
            }
        }
    }
}
