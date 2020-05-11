using System;
using System.Collections.Generic;

namespace Entities
{
    public class MatchResult
    {
        public IReadOnlyList<int> CharacterPositions { get; set; }
        public MatchResultErrorEnum? Error { get; set; }
    }

    public enum MatchResultErrorEnum
    {
        SubtextNullOrEmpty = 1,
    }
}
