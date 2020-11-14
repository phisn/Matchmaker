using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matchmaker.Logic
{
    public static class Teams
    {
        public static Team ByColor(TeamColor color)
            => color == TeamColor.Blue ? Blue : Red;

        public static double GetCurrentScoreForRed()
        {
            using (Context context = new Context())
            {
                List<Participant> red = Teams.Red.Participants
                    .Select(p => context.Participants.Find(p))
                    .ToList();
                List<Participant> blue = Teams.Blue.Participants
                    .Select(p => context.Participants.Find(p))
                    .ToList();

                return Team.GetScoreFor(red, blue);
            }
        }

        public static Team Red { get; } = new Team();
        public static Team Blue { get; } = new Team();
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
