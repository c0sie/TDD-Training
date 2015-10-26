using System;

namespace Training.Service.Exceptions
{
    [Serializable]
    public class DuplicateMatchException : Exception
    {
        public int MatchId { get; private set; }

        public DuplicateMatchException()
        {

        }

        public DuplicateMatchException(int matchId)
            : base(ErrorMessage(matchId))
        {
            MatchId = matchId;
        }

        private static string ErrorMessage(int matchId)
        {
            return $"A match already exists with the supplied details. Match Id: {matchId}";
        }
    }
}
