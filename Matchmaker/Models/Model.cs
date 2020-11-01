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
        public static int DefaultScore { get; } = 1000;

        public Participant()
        {
            Score = DefaultScore;
        }

        public int ParticipantId { get; set; }
        public float Score { get; set; }
        public string Name { get; set; }

        public static Participant FindParticipantByString(string participantName)
        {
            using (Context context = new Context())
            {
                Participant participant = context.Participants
                    .Where((p) => p.Name == participantName.ToLower())
                    .First();

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
